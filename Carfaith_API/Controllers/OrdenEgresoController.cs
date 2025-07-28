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

        [HttpPost("CrearOrdenEgresoConDetalles")]
        public async Task<IActionResult> CrearOrdenEgresoDetalles([FromBody] OrdenEgresoConDetallesDTO ordenEgresoConDetallesDTO)
        {
            try
            {
                var OrdenEgreso = await _ordenEgresoStockServicio.CrearOrdenEgresoConDetalles(ordenEgresoConDetallesDTO);

                return Ok(new
                {
                    message = "Orden de egreso creada exitosamente.",
                    ordenEgreso = OrdenEgreso
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

        [HttpPut("EditarOrdenEgresoConDetalles")]
        public async Task<IActionResult> EditarOrdenEgresoConDetalles([FromBody] OrdenEgresoConDetallesDTO OrdenEgresoConDetallesDTO)
        {
            try
            {
                await _ordenEgresoStockServicio.EditarOrdenEgresoConDetalles(OrdenEgresoConDetallesDTO);
                return Ok(new { message = "Orden de egreso editada exitosamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }


        [HttpGet("ListarOrdenesIngresoConDetalles")]
        public async Task<IActionResult> ListarOrdenesIngresoConDetalles()
        {
            try
            {
                var ordenes = await _ordenEgresoStockServicio.GetOrdenEgresoConDetalles();
                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }

        [HttpDelete("EliminarOrdenEgresoConDetalles/{id}")]
        public async Task<IActionResult> EliminarOrdenEgresoConDetalles(int id)
        {
            try
            {
                await _ordenEgresoStockServicio.EliminarOrdenEgresoConDetalles(id);
                return Ok(new { message = "Orden de egreso eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }
    }
}
