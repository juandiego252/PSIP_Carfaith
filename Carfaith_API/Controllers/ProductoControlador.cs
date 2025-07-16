using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoControlador : ControllerBase
    {
        private IProductoServicio _productoServicio;

        public ProductoControlador(IProductoServicio productoServicio)
        {
            _productoServicio = productoServicio;
        }

        [HttpGet("listarProductos")]
        public Task<IEnumerable<Producto>> ObtenerProductos()
        {
            return _productoServicio.GetAllProductoAsync();
        }

        [HttpPost("crearProducto")]
        public async Task<IActionResult> CrearProductos([FromBody] Producto producto)
        {
            try
            {
                await _productoServicio.AddProductoAsync(producto);

                return Ok(producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var productos = await _productoServicio.GetAllProductoAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("producto/{id}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            try
            {
                var producto = await _productoServicio.GetByIdProductoAsync(id);
                return Ok(producto);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }
    }
}
