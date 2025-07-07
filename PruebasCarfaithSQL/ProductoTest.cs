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
    [Category("productos")]
    public async Task CrearProducto()
    {
        var producto = new Producto
        {
            CodigoProducto = "PROD010",
            Nombre = "Filtro de aire",
            LineaDeProducto = 4 // Asumiendo que la línea de producto con ID 1 ya existe
        };

        await _productoServicio.AddProductoAsync(producto);
    }

}
