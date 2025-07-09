using AppVinhosFinal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            // 1. Obtém o claim com o Id da Quinta
            var quintaIdClaim = User.FindFirst("QuintaId")?.Value;
            if (string.IsNullOrEmpty(quintaIdClaim) || !int.TryParse(quintaIdClaim, out var quintaId))
            {
                return Forbid(); // ou redireciona / mostra erro
            }

            // 2. Query aos vinhos dessa Quinta
            var wines = _context.Vinhos
                .Where(w => w.IdQuinta == quintaId)
                .OrderBy(w => w.Nome)
                .ToList();

            // 3. Passa a lista à view
            return View(wines);
        }
    }
}
