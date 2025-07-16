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
        private readonly IOrdenDeIngresoServicio _ordenDeIngresoServicio;

        public OrdenDeIngresoController(IOrdenDeIngresoServicio ordenDeIngresoServicio)
        {
            _ordenDeIngresoServicio = ordenDeIngresoServicio;
        }

        [HttpPost("CrearOrdenIngreso")]
        public async Task<IActionResult> CrearOrdenIngreso([FromBody] OrdenDeIngreso ordenDeIngreso)
        {
            try
            {
                await _ordenDeIngresoServicio.AddOrdenDeIngresoAsync(ordenDeIngreso);
                return Ok(new { message = "Orden de ingreso creada exitosamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }

        [HttpGet("ListarOrdenesDeIngreso")]
        public async Task<IActionResult> ListarOrdenesDeIngreso()
        {
            try
            {
                var ordenesDeIngreso = await _ordenDeIngresoServicio.OrdenDeIngresoOrdenCompraDTOs();
                return Ok(ordenesDeIngreso);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }

        [HttpPut("EditarOrdenDeIngreso")]
        public async Task<IActionResult> EditarOrdenDeIngreo([FromBody] OrdenDeIngreso ordenDeIngreso)
        {
            try
            {
                await _ordenDeIngresoServicio.UpdateOrdenDeIngresoAsync(ordenDeIngreso);
                return Ok(new { message = "Orden de ingreso actualizada exitosamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }

        [HttpDelete("EliminarOrdenDeIngreso/{id}")]

        public async Task<IActionResult> EliminarOrdenDeIngreso(int id)
        {
            try
            {
                await _ordenDeIngresoServicio.DeleteOrdenDeIngresoByIdAsync(id);
                return Ok(new { message = "Orden de Ingreso Eliminada Correctmente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }
    }
}
