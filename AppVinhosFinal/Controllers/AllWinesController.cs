using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;

namespace AppVinhosFinal.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class AllWinesController : Controller
    {
        private readonly AppDbContext _context;

        public AllWinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /AllWines
        public IActionResult Index()
        {
            var wines = _context.Vinhos
                .Include(w => w.Quinta)
                .Where(w => w.Estado == EstadoVinho.Visible)
                .OrderBy(w => w.Quinta!.Nome)
                .ThenBy(w => w.Nome)
                .ToList();

            // Agrupa por nome da Quinta
            var grouped = wines.GroupBy(w => w.Quinta!.Nome);
            return View(grouped);
        }

        // GET: /AllWines/Details/5
        public IActionResult Details(int id)
        {
            var wine = _context.Vinhos
                .Include(w => w.Quinta)
                .FirstOrDefault(w => w.Id == id);
            if (wine == null) return NotFound();
            return View(wine);
        }

        // GET: /AllWines/Edit/5  (só Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var wine = await _context.Vinhos.FindAsync(id);
            if (wine == null) return NotFound();
            return View(wine);
        }

        // POST: /AllWines/Edit/5  (só Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Quantidade,QuantidadeFria,IdQuinta,Estado")] Vinhos vinho)
        {
            if (id != vinho.Id) return NotFound();

            if (vinho.Quantidade < vinho.QuantidadeFria)
                ModelState.AddModelError("QuantidadeFria", "A quantidade fria não pode ser maior que o total.");

            if (!ModelState.IsValid)
                return View(vinho);

            var entity = await _context.Vinhos.FindAsync(id);
            if (entity == null) return NotFound();

            entity.Nome = vinho.Nome;
            entity.Quantidade = vinho.Quantidade;
            entity.QuantidadeFria = vinho.QuantidadeFria;
            // Estado e QuintaId permanecem inalterados

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
