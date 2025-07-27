using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private IProductoServicio _productoServicio;

        public ProductoController(IProductoServicio productoServicio)
        {
            _productoServicio = productoServicio;
        }

        [HttpPost("CrearProductos")]
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

        [HttpGet("ListarProductos")]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var productos = await _productoServicio.GetProductosResumen();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("ObtenerProductoPorId/{id}")]
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

        [HttpPut("EditarProducto")]
        public async Task<IActionResult> EditarProducto([FromBody] Producto producto)
        {
            try
            {
                await _productoServicio.UpdateProductoAsync(producto);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpDelete("EliminarProducto/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                await _productoServicio.DeleteProductoByIdAsync(id);
                return Ok($"Producto con id {id} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }
    }
}
