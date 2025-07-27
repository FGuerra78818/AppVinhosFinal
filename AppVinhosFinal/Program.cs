using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using AppVinhosFinal.Services;
using AppVinhosFinal.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Resend;
using AppVinhosFinal.Hubs;
using DotNetEnv;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// 1) Carrega as variáveis do .env para o processo ANTES de criar o builder
Env.Load();

// 2) Cria o builder e adiciona também as variáveis de ambiente
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
       .AddEnvironmentVariables(); // garante que .env (via Env.Load) seja lido

// 3) DbContext com Identity, agora a usar a ConnectionString do .env
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4) Identity
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

// 5) Configuração do cookie do Identity
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.Cookie.HttpOnly = true;
    opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    opts.Cookie.SameSite = SameSiteMode.None;
    opts.LoginPath = "/Account/Login";
    opts.AccessDeniedPath = "/Account/Login";
});

// 6) Filtro global de “must change password”
builder.Services.AddScoped<RequirePasswordChangeAttribute>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<RequirePasswordChangeAttribute>();
});

// 7) Resend (e-mail) – as credenciais vêm do .env
builder.Services.Configure<ResendOptions>(builder.Configuration.GetSection("Resend"));
var resendOpts = builder.Configuration.GetSection("Resend").Get<ResendOptions>()!;
builder.Services.Configure<ResendClientOptions>(o => o.ApiToken = resendOpts.Key);
builder.Services.AddHttpClient<ResendClient>();
builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddTransient<IEmailSender, ResendService>();

// 8) SignalR
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Hub real‑time
app.MapHub<NotificationHub>("/notificationHub");

// Rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
