using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;

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
    }
}
