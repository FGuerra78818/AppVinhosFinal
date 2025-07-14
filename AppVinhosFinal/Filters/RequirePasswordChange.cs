using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AppVinhosFinal.Entities;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppVinhosFinal.Filters
{
    public class RequirePasswordChangeAttribute : IAsyncActionFilter
    {
        private readonly AppDbContext _db;

        public RequirePasswordChangeAttribute(AppDbContext db)
        {
            _db = db;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                // obtém o Id do claim
                var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(idClaim, out var userId))
                {
                    // carrega apenas a flag MustChangePassword
                    var mustChange = await _db.Users
                        .Where(u => u.Id == userId)
                        .Select(u => u.MustChangePassword)
                        .FirstOrDefaultAsync();

                    // se estiver true e NÃO estivermos já no ChangePassword
                    var rd = context.RouteData;
                    var ctrl = rd.Values["controller"]?.ToString();
                    var act = rd.Values["action"]?.ToString();
                    var isChangePassword = (ctrl == "Account" && act == "ChangePassword");

                    if (mustChange && !isChangePassword)
                    {
                        // redirecciona para GET ChangePassword
                        context.Result = new RedirectToActionResult(
                            actionName: "ChangePassword",
                            controllerName: "Account",
                            routeValues: null
                        );
                        return;
                    }
                }
            }

            // segue com a acção normalmente
            await next();
        }
    }
}
