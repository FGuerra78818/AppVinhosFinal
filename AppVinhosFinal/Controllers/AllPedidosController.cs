using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AppVinhosFinal.Hubs;
using Microsoft.AspNetCore.SignalR;
using static Azure.Core.HttpHeader;

namespace AppVinhosFinal.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class AllPedidosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public AllPedidosController(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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

        // POST: /AllPedidos/Approve/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            // 1) Carrega o pedido com os vinhos para obter o Id da Quinta, se existir
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoVinhos)
                    .ThenInclude(pv => pv.Vinho)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            // 2) Verifica estado
            if (pedido.Estado != EstadoPedido.PorAprovar)
                return BadRequest("Pedido cancelado pelo utilizador. Não é permitido Aprovar pedidos cancelados.");

            // 3) Altera estado e data de aprovação
            pedido.Estado = EstadoPedido.Aprovado;
            pedido.DataAprovacao = DateTime.UtcNow;

            // 4) Determina o Id da Quinta de forma segura:
            //    - Se existir pelo menos um item, usa o primeiro.
            //    - Caso contrário, recorre à claim do utilizador (quem criou o pedido).
            int QuintaId;
            var primeiro = pedido.PedidoVinhos.FirstOrDefault();
            if (primeiro != null)
            {
                QuintaId = primeiro.Vinho.IdQuinta;
            }
            else if (!int.TryParse(User.FindFirst("QuintaId")?.Value, out QuintaId))
            {
                return BadRequest("Não foi possível determinar a Quinta para notificação.");
            }

            var text = $"Pedido #{pedido.Id} aprovado.";

            // 7) Dispara a notificação a todos os utilizadores ligados a esta Quinta
            await _hubContext
                .Clients
                .Group($"Quinta-{QuintaId}")
                .SendAsync("ReceiveNotification", new
                {
                    Mensagem = text
                });

            _context.Notifications.Add(new Notification
            {
                QuintaId = QuintaId,
                Message = text,
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                Direction = NotificationDirection.AdminToUser
            });

            // 5) Grava a mudança de estado
            await _context.SaveChangesAsync();

            return Ok("Pedido aprovado com sucesso.");
        }

    }
}