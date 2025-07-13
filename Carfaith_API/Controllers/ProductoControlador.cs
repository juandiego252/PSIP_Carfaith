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

        [HttpPost("agregarProducto")]
        public async Task<IActionResult> InsertarDatosProductos([FromBody] Producto producto)
        {
            try
            {
                await _productoServicio.AddProductoAsync(producto);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());

                return StatusCode(500, "Error interno");
            }
        }
    }
}
