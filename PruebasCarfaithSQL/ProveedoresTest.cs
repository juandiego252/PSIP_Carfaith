using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace PruebasCarfaithSQL;

public class ProveedoresTest
{
    private CarfaithDbContext _context;
    private IProveedoresServicio _proveedoresServicio;
    private const string CONNECTION_STRING = "Server=DESKTOP-REELKQG\\SQLEXPRESS;Database=carfaith;User=sa;Password=123456;TrustServerCertificate=True;";
    [SetUp]
    public void Setup()
    {
        var option = new DbContextOptionsBuilder<CarfaithDbContext>()
    .UseSqlServer(CONNECTION_STRING)
    .Options;
        _context = new CarfaithDbContext(option);
        _proveedoresServicio = new ProveedoresServicioImpl(_context);
    }

    [Test]
    [Category("Proveedores")]
    public async Task CrearProveedor()
    {
        var proveedor = new Proveedores
        {
            NombreProveedor = "MotoPartsKin",
            PaisOrigen = "Ecuador",
            TipoProveedor = "Local",
            Telefono = "0987654321",
            Email = "motoParts@google.com",
            PersonaContacto = "Jose Galarza",
            FechaRegistro = DateOnly.FromDateTime(DateTime.Now),
            Ruc = "1234567890",
            Direccion = "Av. Principal 123",
            Estado = true
        };
        await _proveedoresServicio.AddProveedoresAsync(proveedor);

        Assert.Pass("Usuario creado exitosamente");
    }

    [Test]
    [Category("Proveedores")]
    public async Task ObteneProveedoresPorPais()
    {
        var proveedores = await _proveedoresServicio.GetProveedoresPorPais("Ecuador");
        Console.WriteLine($"Cantidad de proveedores encontrados: {proveedores.Count()}");
        Assert.IsNotEmpty(proveedores, "No se encontraron proveedores para el país especificado.");
    }
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
