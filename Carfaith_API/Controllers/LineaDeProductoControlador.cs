using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/lineaProductos")]
    public class LineaDeProductoControlador : ControllerBase
    {
        private readonly ILineasDeProductoServicio _lineaProductoServicio;

        public LineaDeProductoControlador(ILineasDeProductoServicio lineaProductoServicio)
        {
            _lineaProductoServicio = lineaProductoServicio;
        }

        [HttpGet("listarProductos")]
        public Task<IEnumerable<LineasDeProducto>> ObtenerProductos()
        {
            return _lineaProductoServicio.GetAllLineasDeProductoAsync();
        }

        [HttpPost("lineaProducto")]
        public async Task<IActionResult> InsertarLineaProducto([FromBody] LineasDeProducto lineasProducto)
        {
            try
            {
                await _lineaProductoServicio.AddLineasDeProductoAsync(lineasProducto);

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
