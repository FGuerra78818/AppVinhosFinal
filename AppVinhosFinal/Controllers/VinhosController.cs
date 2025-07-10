using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace AppVinhosFinal.Controllers
{
    [Authorize]
    public class WinesController : Controller
    {
        private readonly AppDbContext _context;

        public WinesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var quintaIdClaim = User.FindFirst("QuintaId")?.Value;
            if (string.IsNullOrEmpty(quintaIdClaim)
                || !int.TryParse(quintaIdClaim, out var quintaId))
                return Forbid();

            var wines = _context.Vinhos
                .Where(w =>
                    w.IdQuinta == quintaId
                    && w.Estado == EstadoVinho.Visible   // só visíveis
                )
                .OrderBy(w => w.Nome)
                .ToList();

            return View(wines);
        }

        public IActionResult Create()
        {
            // role e quintas
            var roleClaim = User.FindFirst(ClaimTypes.Role)!.Value;
            ViewBag.CurrentRole = roleClaim;
            ViewBag.CurrentQuintaId = int.Parse(User.FindFirst("QuintaId")!.Value);

            if (roleClaim == "Admin" || roleClaim == "Staff")
            {
                ViewBag.ListQuintas = _context.Quintas
                    .Select(q => new SelectListItem(q.Nome, q.Id.ToString()))
                    .ToList();
            }

            return View(new CreateWineViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWineViewModel model)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role)!.Value;
            var userQuinta = int.Parse(User.FindFirst("QuintaID")!.Value);

            // Repor ViewBags em caso de erro
            ViewBag.CurrentRole = roleClaim;
            ViewBag.CurrentQuintaId = userQuinta;
            if (roleClaim == "Admin" || roleClaim == "Staff")
            {
                ViewBag.ListQuintas = _context.Quintas
                    .Select(q => new SelectListItem(q.Nome, q.Id.ToString()))
                    .ToList();
            }

            if (!ModelState.IsValid)
                return View(model);

            // Decide a que Quinta este vinho vai pertencer
            var quintaId = (roleClaim == "Admin" || roleClaim == "Staff")
                ? model.QuintaId!.Value
                : userQuinta;

            // 1) Verifica existência
            var existing = await _context.Vinhos
                .FirstOrDefaultAsync(w =>
                   w.Nome == model.Nome &&
                   w.IdQuinta == quintaId);

            if (existing != null)
            {
                // existe mas está oculto?
                if (existing.Estado == EstadoVinho.Hidden)
                {
                    // Atualiza o estado
                    existing.Estado = EstadoVinho.Visible;
                    // Atualiza as quantidades
                    existing.Quantidade = model.Quantidade;
                    existing.QuantidadeFria = model.QuantidadeFria;

                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Vinho existente — quantidades atualizadas.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Erro! Já existe um vinho com esse nome.");
                    return View(model);
                }
            }

            // 1) Carrega a quinta e soma a quantidade fria atual
            var quinta = _context.Quintas
                         .Single(q => q.Id == quintaId);
            var somaFriaAtual = _context.Vinhos
                .Where(w => w.IdQuinta == quintaId)
                .Sum(w => w.QuantidadeFria);

            // 2) Verifica se ultrapassa o limite
            if (somaFriaAtual + model.QuantidadeFria > quinta.NumeroMaxVinhoFrio)
            {
                ModelState.AddModelError(
                    nameof(model.QuantidadeFria),
                    $"A soma da QuantidadeFria ({somaFriaAtual + model.QuantidadeFria}) excede o máximo permitido ({quinta.NumeroMaxVinhoFrio})."
                );
                return View(model);
            }

            // 3) Se passar, cria o vinho
            var wine = new Vinhos
            {
                Nome = model.Nome,
                Quantidade = model.Quantidade,
                QuantidadeFria = model.QuantidadeFria,
                IdQuinta = quintaId,
                Estado = EstadoVinho.Visible
            };

            _context.Vinhos.Add(wine);
            _context.SaveChanges();

            TempData["Message"] = "Vinho adicionado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Wines/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinho = await _context.Vinhos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinho == null)
            {
                return NotFound();
            }

            return View(vinho);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vinho = await _context.Vinhos.FindAsync(id);
            if (vinho != null)
            {
                vinho.Estado = EstadoVinho.Hidden;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Wines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vinho = await _context.Vinhos.FindAsync(id);
            if (vinho == null) return NotFound();

            return View(vinho);
        }

        // POST: Wines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Quantidade,QuantidadeFria")] Vinhos vinho)
        {
            if (id != vinho.Id)
                return NotFound();

            // validar que Quantidade total >= QuantidadeFria
            if (vinho.Quantidade < vinho.QuantidadeFria)
            {
                ModelState.AddModelError("QuantidadeFria", "A quantidade fria não pode ser maior que o total.");
            }

            if (!ModelState.IsValid)
                return View(vinho);

            // obter a entidade existente para manter relacionamentos intactos
            var entity = await _context.Vinhos.FindAsync(id);
            if (entity == null)
                return NotFound();

            // atualizar apenas os campos editáveis
            entity.Nome = vinho.Nome;
            entity.Quantidade = vinho.Quantidade;
            entity.QuantidadeFria = vinho.QuantidadeFria;
            // QuantidadeQuente é calculada internamente no modelo

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VinhoExists(vinho.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VinhoExists(int id)
        {
            return _context.Vinhos.Any(e => e.Id == id);
        }

        // GET: Wines/ConfirmRestore/5
        public async Task<IActionResult> ConfirmRestore(int id)
        {
            var vinho = await _context.Vinhos.FindAsync(id);
            if (vinho == null) return NotFound();
            return View(vinho);
        }

        // POST: Wines/ConfirmRestore/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmRestoreConfirmed(int id)
        {
            var vinho = await _context.Vinhos.FindAsync(id);
            if (vinho != null)
            {
                vinho.Estado = EstadoVinho.Visible;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ConfirmEditQuantity), new { id });
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Wines/ConfirmEditQuantity/5
        public async Task<IActionResult> ConfirmEditQuantity(int id)
        {
            var vinho = await _context.Vinhos.FindAsync(id);
            if (vinho == null) return NotFound();
            return View(vinho);
        }

        // POST: Wines/ConfirmEditQuantity/5
        [HttpPost, ActionName("ConfirmEditQuantity")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmEditQuantityConfirmed(int id, string choice)
        {
            // se o utilizador escolher “sim” redireciona para Edit
            if (choice == "yes")
                return RedirectToAction(nameof(Edit), new { id });

            // caso contrário, volta ao index
            return RedirectToAction(nameof(Index));
        }
    }
}
