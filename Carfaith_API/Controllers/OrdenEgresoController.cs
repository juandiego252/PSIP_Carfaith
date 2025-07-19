using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;
using Carfaith.Aplicacion.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenEgresoController : ControllerBase
    {
        private readonly IOrdenEgresoStockServicio _ordenEgresoStockServicio;

        public OrdenEgresoController(IOrdenEgresoStockServicio ordenEgresoStockServicio)
        {
            _ordenEgresoStockServicio = ordenEgresoStockServicio;
        }

        [HttpPost("CrearOrdenEgreso")]
        public async Task<IActionResult> CrearOrdenEgresoDetalles([FromBody] OrdenEgresoConDetallesDTO ordenEgresoConDetallesDTO)
        {
            try
            {
                var OrdenEgresoId = await _ordenEgresoStockServicio.CrearOrdenEgresoConDetalles(ordenEgresoConDetallesDTO);

                return Ok(new
                {
                    message = "Orden de egreso creada exitosamente.",
                    ordenEgresoId = OrdenEgresoId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error al crear la orden de egreso.",
                    error = ex.Message
                });
            }
        }
    }
}
