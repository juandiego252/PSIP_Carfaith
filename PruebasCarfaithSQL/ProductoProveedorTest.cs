using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Dominio.Modelo.Entidades;

namespace PruebasCarfaithSQL;

public class ProductoProveedorTest : TestBase
{
    private IProductoProveedorServicio _productoProveedorServicio;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _productoProveedorServicio = new ProductoProveedorServicioImpl(Context);
    }


    [Test]
    [Category("ProductoProveedor")]
    public async Task CrearProductoProveedor()
    {
        //var productoProveedor = new ProductoProveedor
        //{
        //    IdProducto = 1,
        //    IdProveedor = 3
        //};

        //await _productoProveedorServicio.AddProductoProveedorAsync(productoProveedor);
        //Assert.Pass("ProductoProveedor creado exitosamente.");
    }

    [Test]
    [Category("ProductoProveedor")]
    public async Task ProductoProveedorDetalleDTO()
    {

        var detalles = await _productoProveedorServicio.GetProductoProveedorDetallesAsync();
        foreach (var item in detalles)
        {
            Console.WriteLine($"Id: {item.IdProductoProveedor}, Producto: {item.NombreProducto}, Proveedor: {item.NombreProveedor}, Linea: {item.LineaProducto}");

        }

    }
}
