using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;

namespace AppVinhosFinal.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class AllUsersController : Controller
    {
        private readonly AppDbContext _context;
        public AllUsersController(AppDbContext context) => _context = context;

        // GET: /AllUsers
        public IActionResult Index(string nome, int? quintaId, string role)
        {
            // Se escolheram Staff/Admin/CEO, ignoramos o filtro de Quinta
            if (role == "Staff" || role == "Admin" || role == "CEO")
                quintaId = null;

            // Dropdown de Quintas
            ViewBag.ListQuintas = _context.Quintas
                .Select(q => new SelectListItem(q.Nome, q.Id.ToString()))
                .ToList();

            // Dropdown de Roles
            ViewBag.ListRoles = new[]
            {
                new SelectListItem("User",  "User"),
                new SelectListItem("Staff", "Staff"),
                new SelectListItem("Admin", "Admin"),
                new SelectListItem("CEO",   "CEO")
            }.ToList();

            // Mantém selecções
            ViewBag.SelectedNome = nome;
            ViewBag.SelectedQuinta = quintaId;
            ViewBag.SelectedRole = role;

            // Query
            var query = _context.Users
                .Include(u => u.Quinta)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(u => u.UserName.Contains(nome));
            if (quintaId.HasValue)
                query = query.Where(u => u.QuintaId == quintaId.Value);
            if (!string.IsNullOrWhiteSpace(role))
                query = query.Where(u => u.Role == role);

            var model = query
                .OrderBy(u => u.UserName)
                .ToList()
                .GroupBy(u => u.Role);

            return View(model);
        }
    }
}
