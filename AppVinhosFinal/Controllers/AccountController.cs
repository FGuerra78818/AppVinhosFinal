using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace AppVinhosFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;

        public AccountController(
            AppDbContext context,
            IEmailSender emailSender,
            UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Admin,CEO")]
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        [Authorize]
        public IActionResult Registration()
        {
            var current = _context.Users
                .Include(u => u.Quinta)
                .Single(u => u.UserName == User.Identity!.Name!);

            ViewBag.ListQuintas = _context.Quintas
                .Select(q => new SelectListItem(q.Nome, q.Id.ToString()))
                .ToList();
            ViewBag.ListRoles = new List<string> { "User", "Staff", "Admin" };
            ViewBag.CurrentRole = current.Role;
            ViewBag.CurrentQuintaId = current.QuintaId;
            ViewBag.CurrentQuintaName = current.Quinta?.Nome;

            return View();
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            var current = _context.Users
                .Include(u => u.Quinta)
                .Single(u => u.UserName == User.Identity!.Name!);

            ViewBag.ListQuintas = _context.Quintas
                .Select(q => new SelectListItem(q.Nome, q.Id.ToString()))
                .ToList();
            ViewBag.ListRoles = new List<string> { "User", "Staff", "Admin" };
            ViewBag.CurrentRole = current.Role;
            ViewBag.CurrentQuintaId = current.QuintaId;
            ViewBag.CurrentQuintaName = current.Quinta?.Nome;

            if (!ModelState.IsValid)
                return View(model);

            var tempPassword = GenerateSecurePassword(12);
            var account = new UserAccount
            {
                UserName = model.UserName,
                Email = model.Email,
                Role = (current.Role == "Admin") ? model.Role : "User",
                QuintaId = (current.Role == "Admin") ? model.QuintaId : current.QuintaId
            };

            var result = await _userManager.CreateAsync(account, tempPassword);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
                return View(model);
            }

            // envia email com a password temporária
            var asunto = "A sua conta foi criada";
            var corpo = new StringBuilder()
                .AppendLine("<!DOCTYPE html>")
                .AppendLine("<html>")
                .AppendLine("<head>")
                .AppendLine("  <meta charset='utf-8' />")
                .AppendLine("  <style>")
                .AppendLine("    body { font-family: Arial, sans-serif; line-height: 1.5; color: #333; }")
                .AppendLine("    .header { background: #4CAF50; color: white; padding: 10px; text-align: center; }")
                .AppendLine("    .content { padding: 20px; }")
                .AppendLine("    .footer { font-size: 0.9em; color: #777; text-align: center; padding: 10px; }")
                .AppendLine("    .btn { display: inline-block; padding: 10px 20px; background: #4CAF50; color: white; text-decoration: none; border-radius: 4px; }")
                .AppendLine("    code { background: #f4f4f4; padding: 2px 4px; border-radius: 4px; }")
                .AppendLine("  </style>")
                .AppendLine("</head>")
                .AppendLine("<body>")
                .AppendLine("  <div class='header'>")
                .AppendLine("    <h1>Bem-vindo à AppVinhos</h1>")
                .AppendLine("  </div>")
                .AppendLine("  <div class='content'>")
                .AppendLine($"    <p>Olá <strong>{account.UserName}</strong>,</p>")
                .AppendLine("    <p>A sua conta foi registada com sucesso.</p>")
                .AppendLine("    <p><strong>Password temporária:</strong> <code>" + tempPassword + "</code></p>")
                .AppendLine("    <p>Por favor altere-a no primeiro login</p>")
                .AppendLine("  </div>")
                .AppendLine("  <div class='footer'>")
                .AppendLine("    <p>Se não solicitou esta ação, por favor ignore este email.</p>")
                .AppendLine("  </div>")
                .AppendLine("</body>")
                .AppendLine("</html>")
                .ToString();
            await _emailSender.SendEmailAsync(account.Email, asunto, corpo);

            TempData["Message"] = $"Utilizador {model.UserName} criado com sucesso.";
            return RedirectToAction(nameof(Registration));
        }

        public IActionResult Login() => View();

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);

                if (user.MustChangePassword)
                    return RedirectToAction(nameof(ChangePassword), "Account");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username ou password inválidos.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = User.Identity!.Name;
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword() => View();

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null) return NotFound();

            var changeResult = await _userManager.ChangePasswordAsync(
                user, model.CurrentPassword, model.NewPassword);

            if (!changeResult.Succeeded)
            {
                foreach (var err in changeResult.Errors)
                    ModelState.AddModelError("", err.Description);
                return View(model);
            }

            // update flag
            user.MustChangePassword = false;
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Profile()
        {
            var user = _context.Users
                .Single(u => u.UserName == User.Identity!.Name!);

            var vm = new ProfileViewModel
            {
                Email = user.Email,
                NewUserName = user.UserName
            };
            return View(vm);
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            if (user == null) return NotFound();

            if (!string.Equals(model.NewUserName, user.UserName, StringComparison.OrdinalIgnoreCase)
                && await _userManager.FindByNameAsync(model.NewUserName) != null)
            {
                ModelState.AddModelError(nameof(model.NewUserName),
                    "Já existe um utilizador com esse nome.");
                return View(model);
            }

            user.UserName = model.NewUserName;
            user.Email = model.Email;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            TempData["ProfileMessage"] = "Perfil atualizado com sucesso!";
            return RedirectToAction(nameof(Profile));
        }

        // Conjuntos de caracteres
        private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lower = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string Specials = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        public static string GenerateSecurePassword(int length = 12)
        {
            if (length < 8)
                throw new ArgumentException("O comprimento mínimo da password deve ser 8.", nameof(length));

            // Garante presença de cada tipo
            var required = new[]
            {
            GetRandomChar(Upper),
            GetRandomChar(Lower),
            GetRandomChar(Digits),
            GetRandomChar(Specials),
        };

            // Todos os caracteres permitidos
            var allChars = Upper + Lower + Digits + Specials;

            // Preenche o resto da password
            var pwdChars = new char[length];
            // Coloca primeiro os obrigatórios
            for (int i = 0; i < required.Length; i++)
                pwdChars[i] = required[i];

            // Preenche o resto com caracteres aleatórios de allChars
            for (int i = required.Length; i < length; i++)
                pwdChars[i] = GetRandomChar(allChars);

            // Embaralha para distribuir os obrigatórios
            return Shuffle(pwdChars);
        }

        private static char GetRandomChar(string allowed)
        {
            var buffer = new byte[4];
            RandomNumberGenerator.Fill(buffer);
            // Converte bytes em índice
            var idx = BitConverter.ToUInt32(buffer, 0) % (uint)allowed.Length;
            return allowed[(int)idx];
        }

        private static string Shuffle(char[] array)
        {
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                // seleciona j aleatório entre 0 e i
                var buffer = new byte[4];
                RandomNumberGenerator.Fill(buffer);
                int j = (int)(BitConverter.ToUInt32(buffer, 0) % (uint)(i + 1));

                // troca array[i] e array[j]
                var tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
            }
            return new string(array);
        }
    }
}
