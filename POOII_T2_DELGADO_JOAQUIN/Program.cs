using DataLayer;
using DataLayer.Contexto;
using DomainLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Inventario2025Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("cnx")));

builder.Services.AddScoped<InventarioRepository>();
builder.Services.AddScoped<InventarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inventario}/{action=IndexProducto}/{id?}");

app.Run();
