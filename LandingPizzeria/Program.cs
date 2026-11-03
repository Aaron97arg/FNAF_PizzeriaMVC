using Microsoft.AspNetCore.Identity;
using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Interfaces;
using LandingPizzeria.Persistence.Repo;
using LandingPizzeria.Services.IService;
using LandingPizzeria.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Repositorios
builder.Services.AddScoped<IPizzaRepo, RepoPizza>();
builder.Services.AddScoped<IUsuarioRepo, RepoUsuario>();
builder.Services.AddScoped<IPedidoRepo, RepoPedido>();

// Servicios
builder.Services.AddScoped<IPizzaService, ServicePizza>();
builder.Services.AddScoped<IPedidoService, ServicePedido>();
builder.Services.AddScoped<IUsuarioService, ServiceUsuario>();

// Hash de contraseñas
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
