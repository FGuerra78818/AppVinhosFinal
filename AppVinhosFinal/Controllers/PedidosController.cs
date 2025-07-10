using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppVinhosFinal.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;
        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Pedidos
        public IActionResult Index()
        {
            // só traz os pedidos cuja Quinta dos vinhos pertence ao utilizador
            var quintaIdClaim = User.FindFirst("QuintaId")?.Value;
            if (!int.TryParse(quintaIdClaim, out var quintaId))
                return Forbid();

            var pedidos = _context.Pedidos
                .Include(p => p.PedidoVinhos)
                    .ThenInclude(pv => pv.Vinho)
                .Where(p => p.PedidoVinhos
                    .Any(pv => pv.Vinho.IdQuinta == quintaId))
                .OrderByDescending(p => p.DataPedido)
                .ToList();

            return View(pedidos);
        }

        // GET: /Pedidos/Details/5
        public IActionResult Details(int id)
        {
            var pedido = _context.Pedidos
                .Include(p => p.PedidoVinhos)
                    .ThenInclude(pv => pv.Vinho)
                .SingleOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            // verifica se pertence à Quinta do utilizador
            var quintaIdClaim = User.FindFirst("QuintaId")?.Value;
            if (!int.TryParse(quintaIdClaim, out var quintaId) ||
                !pedido.PedidoVinhos.Any(pv => pv.Vinho.IdQuinta == quintaId))
            {
                return Forbid();
            }

            return View(pedido);
        }

        // GET: /Pedidos/Create
        public IActionResult Create()
        {
            if (!int.TryParse(User.FindFirst("QuintaId")?.Value, out var quintaId))
                return Forbid();

            ViewBag.Vinhos = CarregaVinhos(quintaId);

            var model = new Pedidos();
            model.PedidoVinhos.Add(new PedidoVinho());
            return View(model);
        }

        // POST: /Pedidos/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Pedidos model)
        {
            if (!int.TryParse(User.FindFirst("QuintaId")?.Value, out var quintaId))
                return Forbid();

            // filtra apenas itens com quantidade > 0
            var itens = model.PedidoVinhos?.Where(pv => pv.Quantidade > 0).ToList()
                       ?? new List<PedidoVinho>();
            if (!itens.Any())
                ModelState.AddModelError("", "Tens de adicionar pelo menos um vinho com quantidade.");

            // valida stock
            for (int idx = 0; idx < itens.Count; idx++)
            {
                var pv = itens[idx];
                var vinho = _context.Vinhos.Find(pv.IdVinho);
                if (vinho == null)
                {
                    ModelState.AddModelError("", $"Vinho com ID {pv.IdVinho} não existe.");
                    continue;
                }
                if (pv.Quantidade > vinho.Quantidade)
                {
                    ModelState.AddModelError(
                        $"PedidoVinhos[{idx}].Quantidade",
                        $"Só existem {vinho.Quantidade} unidades de “{vinho.Nome}” disponíveis.");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Vinhos = CarregaVinhos(quintaId);
                model.PedidoVinhos = itens;
                return View(model);
            }

            // guarda pedido
            model.DataPedido = DateTime.UtcNow;
            model.Estado = EstadoPedido.PorAprovar;
            model.PedidoVinhos = itens;
            _context.Pedidos.Add(model);
            _context.SaveChanges(); // gera o Id do pedido

            // desconta stock: primeiro quentes, depois frias
            foreach (var pv in model.PedidoVinhos)
            {
                var vinho = _context.Vinhos.Find(pv.IdVinho)!;

                var stockFrio = vinho.QuantidadeFria;
                var stockTotal = vinho.Quantidade;
                var stockQuente = stockTotal - stockFrio;

                var aRemover = pv.Quantidade;

                // retira primeiro das garrafas quentes
                var removeQuente = Math.Min(aRemover, stockQuente);
                aRemover -= removeQuente;

                // depois retira das frias
                var removeFrio = Math.Min(aRemover, stockFrio);
                aRemover -= removeFrio;

                // atualiza valores
                vinho.QuantidadeFria = stockFrio - removeFrio;
                vinho.Quantidade = stockTotal - (removeQuente + removeFrio);

                _context.Entry(vinho).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // Extrai para método a carregar a lista de vinhos
        private List<SelectListItem> CarregaVinhos(int quintaId)
        {
            return _context.Vinhos
                .Where(v => v.IdQuinta == quintaId && v.Estado == EstadoVinho.Visible)
                .Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = $"{v.Nome} (Disp: {v.Quantidade})"
                })
                .ToList();
        }
    }
}
