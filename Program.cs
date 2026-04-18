using Microsoft.EntityFrameworkCore; // Necesario para UseSqlServer
using MesonConnect.Models;      // Asegúrate de que este sea el namespace de tu ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ... el resto de tu código (UseStaticFiles, UseRouting, etc.) queda igual ...

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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