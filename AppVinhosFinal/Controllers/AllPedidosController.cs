using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppVinhosFinal.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class AllPedidosController : Controller
    {
        private readonly AppDbContext _context;

        public AllPedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /AllPedidos
        public IActionResult Index(int? quintaId, EstadoPedido? estado)
        {
            // Carregar lista de quintas para filtro
            ViewBag.ListQuintas = _context.Quintas
                .Select(q => new SelectListItem
                {
                    Value = q.Id.ToString(),
                    Text = q.Nome
                }).ToList();

            // Lista de estados para filtro
            ViewBag.ListEstados = Enum.GetValues(typeof(EstadoPedido))
                .Cast<EstadoPedido>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            // Query base
            var query = _context.Pedidos
                .Include(p => p.PedidoVinhos)
                    .ThenInclude(pv => pv.Vinho)
                        .ThenInclude(w => w.Quinta)
                .AsQueryable();

            // Aplica filtro por quinta
            if (quintaId.HasValue)
                query = query.Where(p => p.PedidoVinhos
                    .Any(pv => pv.Vinho.IdQuinta == quintaId.Value));

            // Aplica filtro por estado
            if (estado.HasValue)
                query = query.Where(p => p.Estado == estado.Value);

            var pedidos = query
                .OrderByDescending(p => p.DataPedido)
                .ToList();

            // Agrupa por Quinta (se não houver itens, agrupa em "Sem Quinta")
            var grouped = pedidos
                .GroupBy(p => p.PedidoVinhos.Any()
                    ? p.PedidoVinhos.First().Vinho.Quinta!.Nome
                    : "Sem Quinta");

            // Mantém filtros seleccionados
            ViewBag.SelectedQuinta = quintaId;
            ViewBag.SelectedEstado = estado?.ToString();

            return View(grouped);
        }

        // GET: /AllPedidos/Details/5
        public IActionResult Details(int id)
        {
            var pedido = _context.Pedidos
                .Include(p => p.PedidoVinhos)
                    .ThenInclude(pv => pv.Vinho)
                        .ThenInclude(w => w.Quinta)
                .FirstOrDefault(p => p.Id == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }
    }
}