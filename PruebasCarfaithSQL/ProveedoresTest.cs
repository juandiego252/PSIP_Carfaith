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
            NombreProveedor = "Guangzhou Auto Parts Co., Ltd",
            PaisOrigen = "China",
            TipoProveedor = "Internacional",
            Telefono = "+86 2087654321",
            Email = "autoParts@google.com",
            PersonaContacto = "Li Wei Shiao",
            FechaRegistro = DateOnly.FromDateTime(DateTime.Now),
            Ruc = "91310110MA1G8XYW3N",
            Direccion = "Room 302, Building B, No. 1688 Zhongshan Avenue East,\r\nTianhe District, Guangzhou, Guangdong Province,\r\nChina, 510630",
            Estado = true
        };
        await _proveedoresServicio.AddProveedoresAsync(proveedor);

        Assert.Pass("Proveedor creado exitosamente");
    }

    //[Test]
    //[Category("Proveedores")]
    //public async Task ObteneProveedoresPorPais()
    //{
    //    var proveedores = await _proveedoresServicio.GetProveedoresPorPais("Ecuador");
    //    Console.WriteLine($"Cantidad de proveedores encontrados: {proveedores.Count()}");
    //    Assert.IsNotEmpty(proveedores, "No se encontraron proveedores para el país especificado.");
    //}

    //[Test]
    //[Category("Proveedores")]
    //public async Task EliminarProveedores()
    //{
    //    await _proveedoresServicio.DeleteProveedoresByIdAsync(2);
    //    Assert.Pass("Proveedor eliminado exitosamente");
    //}

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
