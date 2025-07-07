using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Dominio.Modelo.Entidades;

namespace PruebasCarfaithSQL;

public class LineasDeProductoTest : TestBase
{
    private ILineasDeProductoServicio _lineasDeProductoServicio;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _lineasDeProductoServicio = new LineasDeProductoServicioImpl(Context);
    }

    [Test]
    [Category("LineasDeProducto")]
    public async Task AgregarLineasProducto()
    {
        var lineasProducto = new LineasDeProducto
        {
            Nombre = "Repuestos para Motocicleta",
            Descripcion = "Todo tipo de repuestos para motocicleta",
        };
        await _lineasDeProductoServicio.AddLineasDeProductoAsync(lineasProducto);
    }
}
