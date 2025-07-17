using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;

namespace AppVinhosFinal.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class LogsCoposController : Controller
    {
        private readonly AppDbContext _context;
        public LogsCoposController(AppDbContext context) => _context = context;

        // GET: /LogsCopos
        // Mostra o form de Create já com os compradores disponíveis
        public IActionResult Index()
        {
            ViewData["Title"] = "Novo Registo de Copos";
            // Carrega compradores existentes para a selectbox
            ViewBag.Compradores = _context.LogsCopos
                .Where(l => l.Comprador != null && l.Comprador != "")
                .Select(l => l.Comprador!)
                .Distinct()
                .ToList();

            return View("Create", new LogsCopos());
        }

        // POST: /LogsCopos
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LogsCopos model)
        {
            ViewData["Title"] = "Novo Registo de Copos";
            // Repõe compradores em caso de erro
            ViewBag.Compradores = _context.LogsCopos
                .Where(l => l.Comprador != null && l.Comprador != "")
                .Select(l => l.Comprador!)
                .Distinct()
                .ToList();

            if (model.QuantidadeVendida < 1)
                ModelState.AddModelError(nameof(model.QuantidadeVendida),
                    "Tem de vender pelo menos um copo.");

            if (!ModelState.IsValid)
                return View("Create", model);

            // define data/hora automática
            model.DataHoraVenda = DateTime.UtcNow;
            _context.LogsCopos.Add(model);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Registo gravado com sucesso!";
            // ao gravar, redireciona de novo para o Create, e o novo comprador (se fornecido) já será incluído
            return RedirectToAction(nameof(Index));
        }
    }
}
