using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TransferenciasController : ControllerBase
    {
        private readonly ITransferenciasServicio _transferenciaServicio;

        public TransferenciasController(ITransferenciasServicio transferenciaServicio)
        {
            _transferenciaServicio = transferenciaServicio;
        }

        [HttpPost("CrearTransferencia")]
        public async Task<IActionResult> CrearTransferencia([FromBody] Transferencias transferencia)
        {
            try
            {
                await _transferenciaServicio.AddTransferenciasAsync(transferencia);
                return Ok(new { message = "Transferencia creada correctamente.", data = transferencia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear la transferencia.", error = ex.Message });
            }
        }

        [HttpGet("ListarTransferencias")]
        public async Task<IActionResult> ObtenerTransferencias()
        {
            try
            {
                var transferencias = await _transferenciaServicio.GetAllTransferenciasAsync();
                return Ok(transferencias);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener el listado de transferencias.", error = ex.Message });
            }
        }

        [HttpGet("ListarTransferencias/{id}")]
        public async Task<IActionResult> ObtenerTransferenciasId(int id)
        {
            try
            {
                var transferencias = await _transferenciaServicio.GetTransferenciasById(id);
                return Ok(transferencias);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener la transferencia buscada.", error = ex.Message });
            }
        }

        [HttpPut("ActualizarTransferencia")]
        public async Task<IActionResult> ActualizarTransferencia([FromBody] Transferencias transferencia)
        {
            try
            {
                await _transferenciaServicio.UpdateTransferenciasAsync(transferencia);
                return Ok(new { message = "Transferencia actualizada correctamente.", data = transferencia });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar la transferencia.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarTransferencia/{id}")]
        public async Task<IActionResult> EliminarTransferencia(int id)
        {
            try
            {
                await _transferenciaServicio.DeleteTransferenciasAsync(id);
                return Ok(new { message = "Transferencia eliminada correctamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al eliminar la transferencia.", error = ex.Message });
            }
        }
    }
}
