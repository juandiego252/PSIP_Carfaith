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

// Configuracion de la base de datos
var connectDB = builder.Configuration.GetConnectionString("ConexionDBCarFaith");
builder.Services.AddDbContext<CarfaithDbContext>(options => options.UseSqlServer(connectDB));

// Inyeccion de dependencias para los servicios
builder.Services.AddScoped<IProductoServicio, ProductoServicioImpl>();
builder.Services.AddScoped<IProveedoresServicio, ProveedoresServicioImpl>();

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
