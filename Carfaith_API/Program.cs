using System.Text;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith_API.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Basic Authentication",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Ingrese sus credenciales de usuario y contraseña",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });
});

// Configuracion de la base de datos
var connectDB = builder.Configuration.GetConnectionString("ConexionDBCarFaith");
builder.Services.AddDbContext<CarfaithDbContext>(options => options.UseSqlServer(connectDB));

// Inyeccion de dependencias para los servicios
builder.Services.AddScoped<IProductoServicio, ProductoServicioImpl>();
builder.Services.AddScoped<IProveedoresServicio, ProveedoresServicioImpl>();
builder.Services.AddScoped<ILineasDeProductoServicio, LineasDeProductoServicioImpl>();
builder.Services.AddScoped<IOrdenDeCompraServicio, OrdenDeCompraServicioImpl>();
builder.Services.AddScoped<IUsuariosServicio, UsuariosServicioImpl>();
builder.Services.AddScoped<IProductoProveedorServicio, ProductoProveedorServicioImpl>();
builder.Services.AddScoped<IOrdenDeIngresoServicio, OrdenDeIngresoServicioImpl>();
builder.Services.AddScoped<IUbicacionesServicio, UbicacionesServicioImpl>();
builder.Services.AddScoped<IStockServicio, StockServicioImpl>();
builder.Services.AddScoped<ITransferenciasServicio, TransferenciasServicioImpl>();
builder.Services.AddScoped<IDetalleTransferenciaServicio, DetalleTransferenciaServicioImpl>();
builder.Services.AddScoped<ITransferenciaStockServicio, TransferenciaStockServicioImpl>();
builder.Services.AddScoped<IOrdenEgresoServicio, OrdenEgresoServicioImpl>();
builder.Services.AddScoped<IOrdenEgresoStockServicio, OrdenEgresoStockServicioImpl>();
builder.Services.AddScoped<IDetalleOrdenEgresoServicio, DetalleOrdenEgresoServicioImpl>();

// Configuracion de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Configuracion de autenticacion
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuthentication", null);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NewPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
