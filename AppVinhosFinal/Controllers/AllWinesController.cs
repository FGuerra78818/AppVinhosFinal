using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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

        // GET: /AllWines/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var vinho = await _context.Vinhos.FindAsync(id);
            if (vinho == null)
                return NotFound();

            return View(vinho);
        }

        // POST: /AllWines/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Quantidade,QuantidadeFria")] Vinhos vinho)
        {
            if (id != vinho.Id)
                return NotFound();

            // 1) Quantidade total ≥ QuantidadeFria
            if (vinho.Quantidade < vinho.QuantidadeFria)
                ModelState.AddModelError(nameof(vinho.QuantidadeFria),
                    "A quantidade fria não pode ser maior que a quantidade total.");

            // 2) Limite global de fria na Quinta
            var entity = await _context.Vinhos
                .Include(w => w.Quinta)
                .FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null)
                return NotFound();

            var somaOutrosFria = await _context.Vinhos
                .Where(w => w.IdQuinta == entity.IdQuinta
                            && w.Estado == EstadoVinho.Visible
                            && w.Id != id)
                .SumAsync(w => w.QuantidadeFria);

            var restanteFrio = entity.Quinta!.NumeroMaxVinhoFrio - somaOutrosFria;
            if (vinho.QuantidadeFria > restanteFrio)
                ModelState.AddModelError(nameof(vinho.QuantidadeFria),
                    $"A quantidade fria não pode exceder {restanteFrio}, que é o espaço restante neste frigorífico.");

            if (!ModelState.IsValid)
                return View(entity);

            // 3) Atualiza campos permitidos
            entity.Nome = vinho.Nome;
            entity.Quantidade = vinho.Quantidade;
            entity.QuantidadeFria = vinho.QuantidadeFria;
            // CapacidadeFria mantém‐se inalterada

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vinhos.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Wines/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.ListQuintas = _context.Quintas
                .Select(q => new SelectListItem(q.Nome, q.Id.ToString()))
                .ToList();

            return View(new CreateWineViewModel());
        }

        // POST: /Wines/Create
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateWineViewModel model)
        {
            // Repõe a lista de quintas em caso de erro
            ViewBag.ListQuintas = _context.Quintas
                .Select(q => new SelectListItem(q.Nome, q.Id.ToString()))
                .ToList();

            // QuintaId nunca é nulo para Admin, mas validamos de qualquer modo
            if (!model.QuintaId.HasValue)
                ModelState.AddModelError(nameof(model.QuintaId),
                    "Tem de selecionar uma Quinta.");

            // 1) Validar que Quantidade total ≥ QuantidadeFria
            if (model.Quantidade < model.QuantidadeFria)
                ModelState.AddModelError(nameof(model.QuantidadeFria),
                    "A quantidade fria não pode exceder o total.");

            // Só prossegue se ainda não houver erros básicos
            if (ModelState.IsValid)
            {
                // 2) Verificar limite de QuantidadeFria na Quinta
                var quinta = await _context.Quintas
                    .FirstOrDefaultAsync(q => q.Id == model.QuintaId.Value);
                if (quinta == null)
                {
                    ModelState.AddModelError("", "Quinta não encontrada.");
                }
                else
                {
                    // soma de fria existente nos vinhos visíveis desta quinta
                    var somaFriaAtual = await _context.Vinhos
                        .Where(w => w.IdQuinta == quinta.Id
                                    && w.Estado == EstadoVinho.Visible)
                        .SumAsync(w => w.QuantidadeFria);

                    var restante = quinta.NumeroMaxVinhoFrio - somaFriaAtual;
                    if (model.QuantidadeFria > restante)
                    {
                        ModelState.AddModelError(nameof(model.QuantidadeFria),
                            $"Só restam {restante} lugares no frio desta Quinta.");
                    }
                }
            }

            if (!ModelState.IsValid)
                return View(model);

            // 3) Cria o vinho
            var vinho = new Vinhos
            {
                Nome = model.Nome,
                Quantidade = model.Quantidade,
                QuantidadeFria = model.QuantidadeFria,
                CapacidadeFria = model.QuantidadeFria,
                IdQuinta = model.QuintaId!.Value,
                Estado = EstadoVinho.Visible
            };

            _context.Vinhos.Add(vinho);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Vinho adicionado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}
