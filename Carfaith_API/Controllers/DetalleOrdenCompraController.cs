using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleOrdenCompraController : ControllerBase
    {
        private readonly IDetalleOrdenCompraServicio _detalleOrdenCompraServicio;

        public DetalleOrdenCompraController(IDetalleOrdenCompraServicio detalleOrdenCompraServicio)
        {
            _detalleOrdenCompraServicio = detalleOrdenCompraServicio;
        }

        [HttpPost("CrearDetalleOrdenCompra")]
        public async Task<IActionResult> CrearDetalleOrdenCompra([FromBody] DetalleOrdenCompra detalleOrdenCompra)
        {
            try
            {
                await _detalleOrdenCompraServicio.AddDetalleOrdenCompra(detalleOrdenCompra);
                return Ok(new
                {
                    message = "Detalle de Orden de Compra creado correctamente",
                    data = detalleOrdenCompra
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear el Detalle de Orden de Compra.", error = ex.Message });
            }
        }

        [HttpGet("ListarDetallesOrdenCompra/{id}")]
        public async Task<IActionResult> ListarDetalleOrdenCompra(int id)
        {
            try
            {
                var DetalleOrdenCompraList = await _detalleOrdenCompraServicio.GetDetalleOrdenCompraById(id);
                if (DetalleOrdenCompraList == null)
                {
                    return NotFound(new { message = "No se encontraron registros del detalles de orden de compra." });
                }
                return Ok(DetalleOrdenCompraList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los detalles de orden de compra.", error = ex.Message });
            }
        }

        [HttpGet("ListarDetallesOrdenCompra")]
        public async Task<IActionResult> ListarDetalleOrdenCompra()
        {
            try
            {
                var detalleOrdenCompraList = await _detalleOrdenCompraServicio.GetAllDetallesOrdenCompraAsync();
                if (detalleOrdenCompraList == null || !detalleOrdenCompraList.Any())
                {
                    return NotFound(new { message = "No se encontraron registros de detalles de orden de compra." });
                }
                return Ok(detalleOrdenCompraList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los detalles de orden de compra.", error = ex.Message });
            }
        }

        [HttpPut("ActualizarDetalleOrdenCompra")]
        public async Task<IActionResult> ActualizarDetalleOrdenCompra([FromBody] DetalleOrdenCompra detalleOrdenCompra)
        {
            try
            {
                await _detalleOrdenCompraServicio.UpdateDetalleOrdenCompra(detalleOrdenCompra);
                return Ok(new
                {
                    message = "Detalle Orden de Compra actualizado correctamente",
                    data = detalleOrdenCompra
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar el Detalle de Orden de Compra.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarDetalleOrdenCompra/{id}")]
        public async Task<IActionResult> EliminarDetalleOrdenCompra(int id)
        {
            try
            {
                await _detalleOrdenCompraServicio.DeleteDetalleOrdenCompra(id);
                return Ok(new { message = "Detalle de Orden de Compra eliminada correctamente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al eliminar el Detalle de Orden de Compra.", error = ex.Message });
            }
        }
    }
}
