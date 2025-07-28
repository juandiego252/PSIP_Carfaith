using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleOrdenEgresoController : ControllerBase
    {
        private readonly IDetalleOrdenEgresoServicio _detalleOrdenEgresoServicio;

        public DetalleOrdenEgresoController(IDetalleOrdenEgresoServicio detalleOrdenEgresoServicio)
        {
            _detalleOrdenEgresoServicio = detalleOrdenEgresoServicio;
        }

        [HttpPost("CrearDetalleOrdenEgreso")]
        public async Task<IActionResult> CrearDetalleOrdenEgreso([FromBody] DetalleOrdenEgreso detalleOrdenEgreso)
        {
            try
            {
                await _detalleOrdenEgresoServicio.AddDetalleOrdenEgreso(detalleOrdenEgreso);
                return Ok(new
                {
                    message = "Detalle de Orden de Egreso creado correctamente",
                    data = detalleOrdenEgreso
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear el detalle de orden de egreso.", error = ex.Message });
            }
        }

        [HttpGet("ListarDetallesOrdenEgreso/{id}")]
        public async Task<IActionResult> ListarDetalleOrdenEgreso(int id)
        {
            try
            {
                var detalleOrdenEgresoList = await _detalleOrdenEgresoServicio.GetDetalleOrdenEgresoById(id);
                if (detalleOrdenEgresoList == null)
                {
                    return NotFound(new { message = "No se encontraron registros del detalles de orden de egreso." });
                }
                return Ok(detalleOrdenEgresoList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los detalles de orden de egreso.", error = ex.Message });
            }
        }

        [HttpGet("ListarDetallesOrdenEgreso")]
        public async Task<IActionResult> ListarDetalleOrdenEgreso()
        {
            try
            {
                var detalleOrdenEgresoList = await _detalleOrdenEgresoServicio.GetAllDetallesOrdenEgresoAsync();
                if (detalleOrdenEgresoList == null || !detalleOrdenEgresoList.Any())
                {
                    return NotFound(new { message = "No se encontraron registros de detalles de orden de egreso." });
                }
                return Ok(detalleOrdenEgresoList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los detalles de orden de egreso.", error = ex.Message });
            }
        }

        [HttpPut("ActualizarDetalleOrdenEgreso")]
        public async Task<IActionResult> ActualizarDetalleOrdenEgreso([FromBody] DetalleOrdenEgreso detalleOrdenEgreso)
        {
            try
            {
                await _detalleOrdenEgresoServicio.UpdateDetalleOrdenEgreso(detalleOrdenEgreso);
                return Ok(new
                {
                    message = "Detalle Orden de Egreso actualizado correctamente",
                    data = detalleOrdenEgreso
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar el detalle de orden de egreso.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarDetalleOrdenEgreso/{id}")]
        public async Task<IActionResult> EliminarDetalleOrdenEgreso(int id)
        {
            try
            {
                await _detalleOrdenEgresoServicio.DeleteDetalleOrdenEgreso(id);
                return Ok(new { message = "Detalle de orden de egreso eliminada correctamente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al eliminar el detalle de orden de egreso.", error = ex.Message });
            }
        }
    }
}
