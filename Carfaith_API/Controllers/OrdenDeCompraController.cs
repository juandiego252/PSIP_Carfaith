using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenCompra;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrdenDeCompraController : Controller
    {
        private readonly IOrdenCompraConDetalleServicio _ordenCompraConDetalle;

        public OrdenDeCompraController(IOrdenCompraConDetalleServicio ordenCompraConDetalle)
        {
            _ordenCompraConDetalle = ordenCompraConDetalle;
        }

        [HttpPost("CrearOrdenCompra")]
        public async Task<IActionResult> CrearOrdenDeCompra([FromBody] OrdenCompraConDetallesDTO ordenDeCompra)
        {
            try
            {
                var OrdenCompra = await _ordenCompraConDetalle.CrearOrdenCompraConDetalles(ordenDeCompra);

                return Ok(new
                {
                    message = "Orden de compra creada exitosamente.",
                    ordenCompra = OrdenCompra
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error al crear la orden de compra.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("ListarOrdenesCompra")]
        public async Task<IActionResult> ListarOrdenesCompra()
        {
            try
            {
                var ordenesCompra = await _ordenCompraConDetalle.GetOrdenCompraConDetalles();
                return Ok(ordenesCompra);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpPut("EditarOrdenCompra")]
        public async Task<IActionResult> EditarOrdenCompra([FromBody] OrdenCompraConDetallesDTO ordenDeCompra)
        {
            try
            {
                await _ordenCompraConDetalle.EditarOrdenCompraConDetalles(ordenDeCompra);
                return Ok(new
                {
                    message = "Orden de compra editada exitosamente.",
                    ordenCompra = ordenDeCompra
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }

        [HttpDelete("EliminarOrdenCompra/{id}")]
        public async Task<IActionResult> EliminarOrdenCompra(int id)
        {
            try
            {
                await _ordenCompraConDetalle.EliminarOrdenCompraConDetalles(id);

                return Ok($"Orden de compra con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor." });
            }
        }
    }
}
