using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectDB = builder.Configuration.GetConnectionString("ConexionDBCarFaith");
builder.Services.AddDbContext<CarfaithDbContext>(options => options.UseSqlServer(connectDB));

// Entidades a usar para las APIS
builder.Services.AddScoped<IProductoServicio, ProductoServicioImpl>();
builder.Services.AddScoped<IProveedoresServicio, ProveedoresServicioImpl>();
builder.Services.AddScoped<ILineasDeProductoServicio, LineasDeProductoServicioImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
