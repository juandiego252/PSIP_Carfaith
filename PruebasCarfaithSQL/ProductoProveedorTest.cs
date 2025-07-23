//using Carfaith.Aplicacion.Servicios;
//using Carfaith.Aplicacion.ServiciosImpl;
//using Carfaith.Dominio.Modelo.Entidades;

//namespace PruebasCarfaithSQL;

//public class ProductoProveedorTest : TestBase
//{
//    private IProductoProveedorServicio _productoProveedorServicio;

//    [SetUp]
//    public override void Setup()
//    {
//        base.Setup();
//        _productoProveedorServicio = new ProductoProveedorServicioImpl(Context);
//    }


//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task CrearProductoProveedor()
//    {
//        //var productoProveedor = new ProductoProveedor
//        //{
//        //    IdProducto = 10,
//        //    IdProveedor = 3
//        //};

//        //await _productoProveedorServicio.AddProductoProveedorAsync(productoProveedor);
//        //Assert.Pass("ProductoProveedor creado exitosamente.");
//    }


//    [Test]
//    [Category("ProductoProveedor")]

//    public async Task EliminarProductoProveedor()
//    {
//        //var idProductoProveedor = 7;
//        //await _productoProveedorServicio.DeleteProductoProveedorAsync(idProductoProveedor);
//        //Assert.Pass("ProductoProveedor eliminado exitosamente.");
//    }



//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task ProductoProveedorDetalleDTO()
//    {

//        var detalles = await _productoProveedorServicio.GetProductoProveedorDetallesAsync();
//        foreach (var item in detalles)
//        {
//            Console.WriteLine(
//                $"Id: {item.IdProductoProveedor}, " +
//                $"Producto: {item.NombreProducto}, " +
//                $"Proveedor: {item.NombreProveedor}, " +
//                $"Linea: {item.LineaProducto}, " +
//                $"Código:{item.CodigoProducto}");

//        }

//    }

//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task ProveedoresConProductos()
//    {
//        var proveedoresProductos = await _productoProveedorServicio.GetProveedoresConProductosAsync();

//        foreach (var proveedor in proveedoresProductos)
//        {
//            Console.WriteLine($"Proveedor: {proveedor.NombreProveedor}, País: {proveedor.PaisOrigen}, RUC: {proveedor.Ruc}, Total Productos: {proveedor.TotalProductos}");
//            foreach (var producto in proveedor.Productos)
//            {
//                Console.WriteLine($"  Producto: {producto.Nombre}, Código: {producto.CodigoProducto}, Línea: {producto.LineaProducto}");
//            }
//        }
//    }

//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task LineasConProveedores()
//    {
//        var lineasConProveedores = await _productoProveedorServicio.GetLineasConProveedoresAsync();

//        Console.WriteLine("\n===== LÍNEAS DE PRODUCTO CON SUS PROVEEDORES =====\n");

//        if (!lineasConProveedores.Any())
//        {
//            Console.WriteLine("No se encontraron líneas de producto con proveedores.");
//            Assert.Fail("No se encontraron líneas de producto con proveedores.");
//            return;
//        }

//        foreach (var linea in lineasConProveedores)
//        {
//            Console.WriteLine($"LÍNEA: {linea.NombreLinea} (ID: {linea.IdLinea})");
//            Console.WriteLine($"  Total Productos: {linea.TotalProductos}");
//            Console.WriteLine($"  Total Proveedores: {linea.TotalProveedores}");

//            if (linea.Proveedores.Any())
//            {
//                Console.WriteLine("  Proveedores:");
//                int contador = 1;
//                foreach (var proveedor in linea.Proveedores)
//                {
//                    Console.WriteLine($"    {contador}. {proveedor.NombreProveedor} ({proveedor.PaisOrigen})");
//                    contador++;
//                }
//            }
//            else
//            {
//                Console.WriteLine("  No hay proveedores para esta línea.");
//            }
//            Console.WriteLine(new string('-', 50));
//        }

//        Assert.That(lineasConProveedores.Count(), Is.GreaterThan(0),
//            "Debe haber al menos una línea de producto con proveedores");
//        foreach (var linea in lineasConProveedores)
//        {
//            Assert.That(linea.NombreLinea, Is.Not.Null.And.Not.Empty,
//                "El nombre de la línea no debe estar vacío");
//            Assert.That(linea.TotalProductos, Is.GreaterThanOrEqualTo(0),
//                "El total de productos debe ser un número no negativo");
//        }
//    }

//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task ProductosPorLineaProveedor()
//    {
//        var productosPorLinea = await _productoProveedorServicio.GetProductosPorLineaProveedorAsync();

//        Console.WriteLine("\n===== PRODUCTOS POR LÍNEA Y PROVEEDOR =====\n");

//        foreach (var lineaProducto in productosPorLinea)
//        {
//            Console.WriteLine($"LÍNEA: {lineaProducto.NombreLinea}");
//            Console.WriteLine($"  Total Productos: {lineaProducto.TotalProductos}");
//            Console.WriteLine($"  Total Proveedores: {lineaProducto.TotalProveedores}");

//            if (lineaProducto.ProductosProveedores.Any())
//            {
//                Console.WriteLine("  Productos por Proveedor:");
//                foreach (var productoProveedor in lineaProducto.ProductosProveedores)
//                {
//                    Console.WriteLine($"    • {productoProveedor.NombreProducto} - {productoProveedor.NombreProveedor}");
//                }
//            }
//            else
//            {
//                Console.WriteLine("  No hay productos asociados a proveedores en esta línea.");
//            }
//            Console.WriteLine(new string('-', 50));
//        }

//        Assert.That(productosPorLinea.Count(), Is.GreaterThan(0),
//            "Debe haber al menos una línea de producto con productos y proveedores");
//    }

//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task ProveedoresProductosPorPais()
//    {
//        var proveedoresPorPais = await _productoProveedorServicio.GetProveedorProductosPorPaisAsync();

//        Console.WriteLine("\n===== PROVEEDORES Y PRODUCTOS POR PAÍS =====\n");

//        foreach (var pais in proveedoresPorPais)
//        {
//            Console.WriteLine($"PAÍS: {pais.PaisOrigen}");
//            Console.WriteLine($"  Total Proveedores: {pais.TotalProveedores}");

//            if (pais.Proveedores.Any())
//            {
//                Console.WriteLine("  Proveedores:");
//                foreach (var proveedor in pais.Proveedores)
//                {
//                    Console.WriteLine($"    • {proveedor.NombreProveedor} - {proveedor.TotalProductos} productos");
//                }
//            }
//            else
//            {
//                Console.WriteLine("  No hay proveedores registrados para este país.");
//            }
//            Console.WriteLine(new string('-', 50));
//        }

//        Assert.That(proveedoresPorPais.Count(), Is.GreaterThan(0),
//            "Debe haber al menos un país con proveedores");
//    }

//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task ProductosProveedoresPorLinea()
//    {
//        int idLinea = 2;

//        var productos = await _productoProveedorServicio.GetProductosProveedoresPorLineaAsync(idLinea);

//        Console.WriteLine($"\n===== PRODUCTOS Y PROVEEDORES PARA LA LÍNEA ID: {idLinea} =====\n");

//        if (!productos.Any())
//        {
//            Console.WriteLine($"No se encontraron productos para la línea con ID {idLinea}.");
//            return;
//        }
//        var nombreLinea = productos.First().LineaProducto;
//        Console.WriteLine($"LÍNEA: {nombreLinea}");
//        Console.WriteLine($"Total productos encontrados: {productos.Count()}");
//        Console.WriteLine();

//        foreach (var item in productos)
//        {
//            Console.WriteLine($"ID Producto-Proveedor: {item.IdProductoProveedor}");
//            Console.WriteLine($"  Producto: {item.NombreProducto} (Código: {item.CodigoProducto})");
//            Console.WriteLine($"  Proveedor: {item.NombreProveedor} ({item.PaisOrigen})");
//            Console.WriteLine(new string('-', 40));
//        }

//        Assert.That(productos.Count(), Is.GreaterThan(0),
//            $"Debe haber al menos un producto para la línea con ID {idLinea}");
//    }

//    [Test]
//    [Category("ProductoProveedor")]
//    public async Task ProductosProveedoresPorPais()
//    {
//        string paisOrigen = "China";
//        var productos = await _productoProveedorServicio.GetProductosProveedoresPorPaisAsync(paisOrigen);

//        Console.WriteLine($"\n===== PRODUCTOS Y PROVEEDORES DE {paisOrigen.ToUpper()} =====\n");

//        if (!productos.Any())
//        {
//            Console.WriteLine($"No se encontraron productos de proveedores de {paisOrigen}.");
//            return;
//        }

//        Console.WriteLine($"Total productos encontrados: {productos.Count()}");
//        Console.WriteLine();

//        var porProveedor = productos.GroupBy(p => p.NombreProveedor);

//        foreach (var grupoProveedor in porProveedor)
//        {
//            Console.WriteLine($"PROVEEDOR: {grupoProveedor.Key}");

//            foreach (var item in grupoProveedor)
//            {
//                Console.WriteLine($"  • {item.NombreProducto} (Código: {item.CodigoProducto}) - Línea: {item.LineaProducto}");
//            }
//            Console.WriteLine(new string('-', 40));
//        }

//        Assert.That(productos.Count(), Is.GreaterThan(0),
//            $"Debe haber al menos un producto para proveedores de {paisOrigen}");
//    }
//}
