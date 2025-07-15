using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrdenDeCompraController : Controller
    {
        private readonly IOrdenDeCompraServicio _ordenDeCompraServicio;

        public OrdenDeCompraController(IOrdenDeCompraServicio ordenDeCompraServicio)
        {
            _ordenDeCompraServicio = ordenDeCompraServicio;
        }

        [HttpPost("crearOrdenDeCompra")]
        public async Task<IActionResult> CrearOrdenDeCompra([FromBody] OrdenDeCompra ordenDeCompra)
        {
            try
            {
                await _ordenDeCompraServicio.AddOrdenDeCompraAsync(ordenDeCompra);
                return Ok(ordenDeCompra);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("ListarOrdenesCompra")]
        public async Task<IActionResult> ListarOrdenesCompra()
        {
            try
            {
                var ordenesCompra = await _ordenDeCompraServicio.GetAllOrdenDeCompraAsync();
                return Ok(ordenesCompra);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("ObtenerOrdenCompraPorId/{id}")]
        public async Task<IActionResult> ObtenerOrdenePorId(int id)
        {
            try
            {
                var ordenDeCompra = await _ordenDeCompraServicio.GetByIdOrdenDeCompraAsync(id);
                if (ordenDeCompra == null)
                {
                    return NotFound($"Orden de compra con ID {id} no encontrada.");
                }

                return Ok(ordenDeCompra);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");

            }
        }

        [HttpPut("EditarOrdenCompra")]
        public async Task<IActionResult> EditarOrdenCompra([FromBody] OrdenDeCompra ordenDeCompra)
        {
            try
            {
                await _ordenDeCompraServicio.UpdateOrdenDeCompraAsync(ordenDeCompra);
                return Ok(ordenDeCompra);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpDelete("EliminarOrdenCompra/{id}")]
        public async Task<IActionResult> EliminarOrdenCompra(int id)
        {
            try
            {
                await _ordenDeCompraServicio.DeleteOrdenDeCompraByIdAsync(id);
                return Ok($"Orden de compra con ID {id} eliminada correctamente.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }
    }
}
