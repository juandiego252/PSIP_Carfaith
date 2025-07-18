using Carfaith.Aplicacion.DTO.DTOs.Transferencias;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [Route("api/TransferenciasDetalle")]
    [ApiController]
    public class DetalleTransferenciaController : ControllerBase
    {
        private readonly IDetalleTransferenciaServicio _detalleTransferenciaServicio;
        private readonly ITransferenciaStockServicio _transferenciaStockServicio;

        public DetalleTransferenciaController(
            IDetalleTransferenciaServicio detalleTransferenciaServicio,
            ITransferenciaStockServicio transferenciaStockServicio)
        {
            _detalleTransferenciaServicio = detalleTransferenciaServicio;
            _transferenciaStockServicio = transferenciaStockServicio;
        }

        [HttpPost("ProcesarTransferencia")]
        public async Task<IActionResult> ProcesarTransferenciak([FromBody] TransferenciaStockDTO transferenciaStockDTO)
        {
            try
            {
                var idTransferencia = await _transferenciaStockServicio.ProcesarTransferenciaCompleta(transferenciaStockDTO);
                return Ok(new
                {
                    message = "Transferencia procesada correctamente.",
                    idTransferencia = idTransferencia
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = "Error: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor: " + ex.Message });
            }
        }

        [HttpGet("LisstarTransferencias")]

        public async Task<IActionResult> ListarTransferencias()
        {
            try
            {
                var transferencias = await _transferenciaStockServicio.GetTransferenciasConDetalles();
                return Ok(transferencias);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener el listado de transferencias.", error = ex.Message });
            }
        }
    }
}
