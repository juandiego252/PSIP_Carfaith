using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleOrdenIngresoController : ControllerBase
    {
        private readonly IDetalleOrdenIngresoServicio _detalleOrdenIngresoServicio;

        public DetalleOrdenIngresoController(IDetalleOrdenIngresoServicio detalleOrdenIngresoServicio)
        {
            _detalleOrdenIngresoServicio = detalleOrdenIngresoServicio;
        }

        [HttpPost("CrearDetalleOrdenIngreso")]
        public async Task<IActionResult> CrearDetalleOrdenIngreso([FromBody] DetalleOrdenIngreso detalleOrdenIngreso)
        {
            try
            {
                await _detalleOrdenIngresoServicio.AddDetalleOrdenIngresoAsync(detalleOrdenIngreso);
                return Ok(new
                {
                    message = "Detalle de Orden de Ingreso creado correctamente",
                    data = detalleOrdenIngreso
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear el detalle de orden de ingreso.", error = ex.Message });
            }
        }

        [HttpGet("ListarDetallesOrdenIngreso/{id}")]
        public async Task<IActionResult> ListarDetalleOrdenIngreso(int id)
        {
            try
            {
                var detalleOrdenIngresoList = await _detalleOrdenIngresoServicio.GetByIdDetalleOrdenIngresoAsync(id);
                if (detalleOrdenIngresoList == null)
                {
                    return NotFound(new { message = "No se encontraron registros del detalles de orden de ingreso." });
                }
                return Ok(detalleOrdenIngresoList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los detalles de orden de ingreso.", error = ex.Message });
            }
        }

        [HttpGet("ListarDetallesOrdenIngreso")]
        public async Task<IActionResult> ListarDetalleOrdenIngreso()
        {
            try
            {
                var detalleOrdenIngresoList = await _detalleOrdenIngresoServicio.GetAllDetalleOrdenIngresoAsync();
                if (detalleOrdenIngresoList == null || !detalleOrdenIngresoList.Any())
                {
                    return NotFound(new { message = "No se encontraron registros de detalles de orden de ingreso." });
                }
                return Ok(detalleOrdenIngresoList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los detalles de orden de ingreso.", error = ex.Message });
            }
        }

        [HttpPut("ActualizarDetalleOrdenIngreso")]
        public async Task<IActionResult> ActualizarDetalleOrdenIngreso([FromBody] DetalleOrdenIngreso detalleOrdenIngreso)
        {
            try
            {
                await _detalleOrdenIngresoServicio.UpdateDetalleOrdenIngresoAsync(detalleOrdenIngreso);
                return Ok(new
                {
                    message = "Detalle Orden de ingreso actualizado correctamente",
                    data = detalleOrdenIngreso
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar el detalle de orden de ingreso.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarDetalleOrdenIngreso/{id}")]
        public async Task<IActionResult> EliminarDetalleOrdenIngreso(int id)
        {
            try
            {
                await _detalleOrdenIngresoServicio.DeleteDetalleOrdenIngresoByIdAsync(id);
                return Ok(new { message = "Detalle de orden de ingreso eliminado correctamente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al eliminar el detalle de orden de ingreso.", error = ex.Message });
            }
        }
    }
}
