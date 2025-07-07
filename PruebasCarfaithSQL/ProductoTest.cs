using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Dominio.Modelo.Entidades;

namespace PruebasCarfaithSQL;

public class ProductoTest : TestBase
{
    private IProductoServicio _productoServicio;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _productoServicio = new ProductoServicioImpl(Context);
    }


    [Test]
    public async Task CrearProducto()
    {
        //var producto = new Producto
        //{
        //    CodigoProducto = "PROD001",
        //    Nombre = "Decorador Faros de luz",
        //    LineaDeProducto = 1 // Asumiendo que la línea de producto con ID 1 ya existe
        //};

        //await _productoServicio.AddProductoAsync(producto);
    }

}
