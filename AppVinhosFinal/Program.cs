using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using AppVinhosFinal.Services;
using AppVinhosFinal.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Resend;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext com Identity
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Identity (regista automaticamente o cookie “Identity.Application”)
builder.Services.AddIdentity<UserAccount, IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<
    IUserClaimsPrincipalFactory<UserAccount>,
    AppClaimsPrincipalFactory>();

// 3. Configuração do cookie do Identity
builder.Services.ConfigureApplicationCookie(opts =>
{
    // NÃO mudar o esquema — é o Identity.Application
    opts.Cookie.HttpOnly = true;
    opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    opts.Cookie.SameSite = SameSiteMode.None;   // Em localhost, pode precisar de None
    opts.LoginPath = "/Account/Login";
    opts.AccessDeniedPath = "/Account/Login";
});

// 4. Filtro global de “must change password”
builder.Services.AddScoped<RequirePasswordChangeAttribute>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<RequirePasswordChangeAttribute>();
});

// 5. Resend (e-mail)
builder.Services.Configure<ResendOptions>(builder.Configuration.GetSection("Resend"));
var resendOpts = builder.Configuration.GetSection("Resend").Get<ResendOptions>()!;
builder.Services.Configure<ResendClientOptions>(o => o.ApiToken = resendOpts.Key);
builder.Services.AddHttpClient<ResendClient>();
builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddTransient<IEmailSender, ResendService>();

var app = builder.Build();

// --- Pipeline ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** Aqui o Identity regista o handler “Identity.Application” **
app.UseAuthentication();
app.UseAuthorization();

// Rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
