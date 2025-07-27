using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController : ControllerBase
    {
        private readonly IUbicacionesServicio _ubicacionesServicio;

        public UbicacionesController(IUbicacionesServicio ubicacionesServiciok)
        {
            _ubicacionesServicio = ubicacionesServiciok;
        }

        [HttpPost("CrearUbicacion")]
        public async Task<IActionResult> CrearUbicacion([FromBody] Ubicaciones ubicacion)
        {
            try
            {
                await _ubicacionesServicio.AddUbicacionesAsync(ubicacion);
                return Ok(new { message = "Ubicación creada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear la ubicación.", error = ex.Message });
            }
        }
        [HttpGet("ListarUbicaciones")]

        public async Task<IActionResult> ListarUbicaciones()
        {
            try
            {
                var ubicaciones = await _ubicacionesServicio.GetAllUbicacionesAsync();
                return Ok(ubicaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al listar las ubicaciones.", error = ex.Message });
            }
        }

    }
}
