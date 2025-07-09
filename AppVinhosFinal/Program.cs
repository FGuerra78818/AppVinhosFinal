using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using AppVinhosFinal.Services;
using AppVinhosFinal.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Resend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<RequirePasswordChangeAttribute>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<RequirePasswordChangeAttribute>();
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

//Resend DI
builder.Services.Configure<ResendOptions>(builder.Configuration.GetSection("Resend"));
ResendOptions resendOptions = builder.Configuration.GetSection("Resend").Get<ResendOptions>()!;
builder.Services.Configure<ResendClientOptions>(o =>
{
    o.ApiToken = resendOptions.Key;
});
builder.Services.AddHttpClient<ResendClient>();
builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddTransient<IEmailSender, ResendService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
