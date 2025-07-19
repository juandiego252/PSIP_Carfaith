using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PreciosHistoricosController : ControllerBase
    {
        private readonly IPreciosHistoricosServicio _preciosHistoricosServicio;

        public PreciosHistoricosController(IPreciosHistoricosServicio preciosHistoricosServicio)
        {
            _preciosHistoricosServicio = preciosHistoricosServicio;
        }

        [HttpPost("CrearPreciosHistoricos")]
        public async Task<IActionResult> CrearPreciosHistoricos([FromBody] PreciosHistoricos preciosHistoricos)
        {
            try
            {
                await _preciosHistoricosServicio.AddPreciosHistoricosAsync(preciosHistoricos);
                return Ok(new
                {
                    message = "Precio Historico creado correctamente",
                    data = preciosHistoricos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear el precio historico.", error = ex.Message });
            }
        }

        [HttpGet("ListarPrecioHistorico/{id}")]
        public async Task<IActionResult> ListarPrecioHistoricoId(int id)
        {
            try
            {
                var PreciosHistoricosList = await _preciosHistoricosServicio.GetByIdPreciosHistoricosAsync(id);
                if (PreciosHistoricosList == null)
                {
                    return NotFound(new { message = "No se encontraron registros de precios historicos." });
                }
                return Ok(PreciosHistoricosList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los precios historicos.", error = ex.Message });
            }
        }

        [HttpGet("ListarPreciosHistoricos")]
        public async Task<IActionResult> ListarPreciosHistoricos()
        {
            try
            {
                var PreciosHistoricosList = await _preciosHistoricosServicio.GetAllPreciosHistoricosAsync();
                if (PreciosHistoricosList == null || !PreciosHistoricosList.Any())
                {
                    return NotFound(new { message = "No se encontraron precios historicos." });
                }
                return Ok(PreciosHistoricosList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los precios historicos.", error = ex.Message });
            }
        }

        [HttpPut("ActualizarPreciosHistoricos")]
        public async Task<IActionResult> ActualizarPreciosHistoricos([FromBody] PreciosHistoricos preciosHistoricos)
        {
            try
            {
                await _preciosHistoricosServicio.UpdatePreciosHistoricosAsync(preciosHistoricos);
                return Ok(new
                {
                    message = "Precio Historico actualizado correctamente",
                    data = preciosHistoricos
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar el precio historico.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarPreciosHistoricos/{id}")]
        public async Task<IActionResult> EliminarPreciosHistoricos(int id)
        {
            try
            {
                await _preciosHistoricosServicio.DeletePreciosHistoricosByIdAsync(id);
                return Ok(new { message = "Precio historico eliminado correctamente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al eliminar el precio historico.", error = ex.Message });
            }
        }
    }
}
