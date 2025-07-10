using Microsoft.AspNetCore.Identity.UI.Services;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System;
using System.Linq;
using System.Text;
using AppVinhosFinal.Services;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AppVinhosFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public AccountController(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_context.UserAccounts.ToList());
        }

        [Authorize]
        public IActionResult Registration()
        {
            // 1. Recupera o utilizador actual
            var currentUserName = User.Identity!.Name!;
            var current = _context.UserAccounts
                            .Include(u => u.Quinta)
                            .Single(u => u.UserName == currentUserName);

            // 2. Preenche o ViewBag.ListQuintas com todas as quintas
            ViewBag.ListQuintas = _context.Quintas
                                    .Select(q => new SelectListItem
                                    {
                                        Text = q.Nome,
                                        Value = q.Id.ToString()
                                    })
                                    .ToList();

            // 3. ViewBag.ListRoles sempre fixa
            ViewBag.ListRoles = new List<string> { "User", "Staff", "Admin" };

            // 4. Passa ao View o QuintaId e Role predefinidos para User/Staff
            ViewBag.CurrentRole = current.Role;
            ViewBag.CurrentQuintaId = current.QuintaId;
            ViewBag.CurrentQuintaName = current.Quinta?.Nome;

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            // Recarrega o current para lógica
            var currentUserName = User.Identity!.Name!;
            var current = _context.UserAccounts
                            .Include(u => u.Quinta)
                            .Single(u => u.UserName == currentUserName);

            // Repõe os ViewBags em caso de erro
            ViewBag.ListQuintas = _context.Quintas
                                    .Select(q => new SelectListItem
                                    {
                                        Text = q.Nome,
                                        Value = q.Id.ToString()
                                    })
                                    .ToList();

            ViewBag.ListRoles = new List<string> { "User", "Staff", "Admin" };
            ViewBag.CurrentRole = current.Role;
            ViewBag.CurrentQuintaId = current.QuintaId;
            ViewBag.CurrentQuintaName = current.Quinta?.Nome;

            if (!ModelState.IsValid)
                return View(model);

            // Gerar password aleatória
            var password = GenerateSecurePassword(12);

            var account = new UserAccount
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = password
            };

            // Lógica de atribuição de Role e QuintaId
            if (current.Role == "Admin")
            {
                account.Role = model.Role;
                account.QuintaId = model.QuintaId;
            }
            else
            {
                account.Role = "User";
                account.QuintaId = current.QuintaId;
            }

            try
            {
                _context.UserAccounts.Add(account);
                _context.SaveChanges();

                // Enviar email com a password
                var assunto = "A sua conta foi criada";
                var corpo = new StringBuilder()
                    .AppendLine($"Olá {account.UserName},")
                    .AppendLine()
                    .AppendLine("A sua conta foi registada com sucesso.")
                    .AppendLine($"Password temporária: {password}")
                    .AppendLine()
                    .AppendLine("Por favor altere-a no primeiro login.")
                    .ToString();

                await _emailSender.SendEmailAsync(account.Email, assunto, corpo);

                TempData["Message"] = $"Utilizador {model.UserName} criado com sucesso.";
                return RedirectToAction(nameof(Registration));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Erro ao registar. Tente novamente.");
                return View(model);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserAccounts.Where(u => u.UserName == model.UserName && u.Password == model.Password).FirstOrDefault();
                
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim("QuintaID", user.QuintaId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if (user.MustChangePassword)
                        return RedirectToAction(nameof(ChangePassword));

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userName = User.Identity!.Name!;
            var account = _context.UserAccounts
                           .Single(u => u.UserName == userName);

            if (account.Password != model.CurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Password actual incorreta.");
                return View(model);
            }

            account.Password = model.NewPassword;
            account.MustChangePassword = false;
            _context.SaveChanges();

            return Redirect("~/");
        }

        // GET: /Account/Profile
        [Authorize]
        public IActionResult Profile()
        {
            var currentUserName = User.Identity!.Name!;
            var account = _context.UserAccounts
                                  .SingleOrDefault(u => u.UserName == currentUserName);
            if (account == null) return NotFound();

            var vm = new ProfileViewModel
            {
                Email = account.Email,
                NewUserName = account.UserName
            };
            return View(vm);
        }

        // POST: /Account/Profile
        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var currentUserName = User.Identity!.Name!;
            var account = _context.UserAccounts
                                  .SingleOrDefault(u => u.UserName == currentUserName);
            if (account == null) return NotFound();

            // validar uniqueness de UserName
            if (!string.Equals(model.NewUserName, account.UserName, StringComparison.OrdinalIgnoreCase))
            {
                var exists = _context.UserAccounts
                                     .Any(u => u.UserName == model.NewUserName);
                if (exists)
                {
                    ModelState.AddModelError(nameof(model.NewUserName),
                        "Já existe um utilizador com esse nome.");
                    return View(model);
                }
            }

            // atualiza campos
            account.UserName = model.NewUserName;
            account.Email = model.Email;
            _context.SaveChanges();

            // Atualiza o cookie de autenticação com o novo UserName
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim("QuintaID", account.QuintaId.ToString())
            };

            var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(ci));

            TempData["ProfileMessage"] = "Perfil atualizado com sucesso!";
            return RedirectToAction(nameof(Profile));
        }

        private string GenerateSecurePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            using var rng = new RNGCryptoServiceProvider();
            var data = new byte[length];
            rng.GetBytes(data);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                var idx = data[i] % chars.Length;
                result[i] = chars[idx];
            }
            return new string(result);
        }
    }
}
