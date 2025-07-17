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

        [HttpPost]
        public async Task<IActionResult> CrearTransferencia([FromBody] Transferencias transferencia)
        {
            try
            {
                await _transferenciaServicio.AddTransferenciasAsync(transferencia);
                return Ok(new { message = "Transferencia creada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear la transferencia.", error = ex.Message });
            }
        }
    }
}
