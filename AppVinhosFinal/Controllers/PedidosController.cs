using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using AppVinhosFinal.Hubs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace AppVinhosFinal.Controllers
{
    [Authorize(Roles = "User")]
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public PedidosController(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
        public async Task<IActionResult> Create(Pedidos model)
        {
            if (!int.TryParse(User.FindFirst("QuintaId")?.Value, out var quintaId))
                return Forbid();

            var quinta = await _context.Quintas.FindAsync(quintaId);
            var nomeDaQuinta = quinta?.Nome;

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

            // Notifica o hub de que há um novo pedido
            await _hubContext
                .Clients
                .Group("Admins")
                .SendAsync("ReceiveNotification", new
                {
                    Mensagem = $"{nomeDaQuinta} criou o Pedido #{model.Id}."
            });

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

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Cancel(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoVinhos)
                    .ThenInclude(pv => pv.Vinho)
                        .ThenInclude(v => v.Quinta)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            var userQuintaId = int.Parse(User.FindFirst("QuintaId")!.Value);
            if (!pedido.PedidoVinhos.Any(pv => pv.Vinho.IdQuinta == userQuintaId))
                return Forbid("Não tens permissão para cancelar este pedido.");

            var userquinta = await _context.Quintas.FindAsync(userQuintaId);
            var nomeDaQuinta = userquinta?.Nome ?? "desconhecida";

            if (pedido.Estado != EstadoPedido.PorAprovar)
                return BadRequest("Pedido já aprovado. Não é permitido cancelar pedidos já cancelados.");

            // Para cada item, repõe o total e tenta inserir o máximo no frio:
            foreach (var item in pedido.PedidoVinhos)
            {
                var wine = item.Vinho!;
                var quinta = wine.Quinta!;

                // 1) Repor o total
                wine.Quantidade += item.Quantidade;

                // 2) Calcular espaço livre no frigorífico da Quinta
                var currentCold = await _context.Vinhos
                    .Where(w => w.IdQuinta == quinta.Id && w.Estado == EstadoVinho.Visible)
                    .SumAsync(w => w.QuantidadeFria);

                var fridgeSpace = Math.Max(0, quinta.NumeroMaxVinhoFrio - currentCold);

                // 3) Quanto ainda cabe neste vinho até à sua capacidade inicial
                var wineCapacityLeft = Math.Max(0, wine.CapacidadeFria - wine.QuantidadeFria);

                // 4) Quantas garrafas deste item podemos colocar no frio
                var toCold = Math.Min(item.Quantidade, Math.Min(wineCapacityLeft, fridgeSpace));

                if (toCold > 0)
                {
                    wine.QuantidadeFria += toCold;
                }

                // 5) Se esgotámos o espaço global do frigorífico, atualiza a capacidade deste vinho
                //    para o que efectivamente ficou no frio
                if (toCold < wineCapacityLeft && fridgeSpace == toCold)
                {
                    wine.CapacidadeFria = wine.QuantidadeFria;
                }
            }

            // 6) Marca como cancelado e grava
            pedido.Estado = EstadoPedido.Cancelado;
            await _context.SaveChangesAsync();

            await _hubContext
                .Clients
                .Group("Admins")
                .SendAsync("ReceiveNotification", new
                {
                    Mensagem = $"{nomeDaQuinta} cancelou o pedido #{pedido.Id}."
            });

            return Ok("Pedido cancelado e stock reposto com sucesso.");
        }
    }
}
