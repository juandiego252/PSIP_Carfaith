using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenIngreso;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenDeIngresoController : ControllerBase
    {
        private readonly IOrdenIngresoStockServicio _ordenIngresoStockServicio;

        public OrdenDeIngresoController(IOrdenIngresoStockServicio ordenIngresoStockServicio)
        {
            _ordenIngresoStockServicio = ordenIngresoStockServicio;
        }

        [HttpPost("CrearOrdenIngresoConDetalles")]
        public async Task<IActionResult> CrearOrdenIngreso([FromBody] OrdenIngresoConDetallesDTO ordenIngresoConDetalles)
        {
            try
            {
                await _ordenIngresoStockServicio.CrearOrdenIngresoConDetalles(ordenIngresoConDetalles);
                return Ok(new { message = "Orden de ingreso creada exitosamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }
    }
}
