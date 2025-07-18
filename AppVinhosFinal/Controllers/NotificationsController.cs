using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;

namespace AppVinhosFinal.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class NotificationsController : Controller
    {
        private readonly AppDbContext _context;
        public NotificationsController(AppDbContext context)
            => _context = context;

        // GET: /Notifications
        public async Task<IActionResult> Index()
        {
            // Verifica o papel
            var isAdminOrStaff = User.IsInRole("Admin") || User.IsInRole("Staff");

            IQueryable<Notification> query = _context.Notifications;

            if (isAdminOrStaff)
            {
                // Admin/Staff vê só notificações de User → Admin (de todas as quintas)
                query = query
                    .Where(n => n.Direction == NotificationDirection.UserToAdmin);
            }
            else
            {
                // User normal: precisa de QuintaId e vê só Admin → User daquela quinta
                var claim = User.FindFirst("QuintaId")?.Value;
                if (!int.TryParse(claim, out var quintaId))
                    return Forbid();

                query = query
                    .Where(n => n.Direction == NotificationDirection.AdminToUser
                             && n.QuintaId == quintaId);
            }

            var notificacoes = await query
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notificacoes);
        }

        // POST: /Notifications/MarkRead/5
        [HttpPost, Authorize]
        public async Task<IActionResult> MarkRead(int id)
        {
            var notif = await _context.Notifications.FindAsync(id);
            if (notif != null)
            {
                notif.IsRead = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MarkAllRead()
        {
            var user = User;
            bool isAdmin = user.IsInRole("Admin") || user.IsInRole("Staff");

            var query = _context.Notifications.AsQueryable();

            if (isAdmin)
            {
                query = query.Where(n => n.Direction == NotificationDirection.UserToAdmin);
            }
            else
            {
                int qid = int.Parse(user.FindFirst("QuintaId")!.Value);
                query = query.Where(n => n.Direction == NotificationDirection.AdminToUser && n.QuintaId == qid);
            }

            var notifs = await query.ToListAsync();
            notifs.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
