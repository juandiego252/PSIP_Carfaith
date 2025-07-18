using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockServicio _stockServicio;

        public StockController(IStockServicio stockServicio)
        {
            _stockServicio = stockServicio;
        }

        [HttpPost("CrearStock")]
        public async Task<IActionResult> CrearStock([FromBody] Stock stock)
        {
            try
            {
                await _stockServicio.AddStockAsync(stock);
                return Ok(new
                {
                    message = "Stock creado correctamente",
                    data = stock
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear el stock.", error = ex.Message });
            }
        }

        [HttpGet("ListarInfoStock")]
        public async Task<IActionResult> ListarInfoStock()
        {
            try
            {
                var stockList = await _stockServicio.GetstockProductoProveedorUbicacionDTOs();
                if (stockList == null || !stockList.Any())
                {
                    return NotFound(new { message = "No se encontraron registros de stock." });
                }
                return Ok(stockList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener los registros de stock.", error = ex.Message });
            }
        }


        [HttpPut("ActualizarStock")]
        public async Task<IActionResult> EditarStock([FromBody] Stock stock)
        {
            try
            {
                await _stockServicio.UpdateStockAsync(stock);
                return Ok(new { message = "Stock actualizado correctamente", data = stock });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar el stock.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarStock/{id}")]
        public async Task<IActionResult> EliminarStock(int id)
        {
            try
            {
                await _stockServicio.DeleteStockAsync(id);
                return Ok(new { message = "Stock eliminado correctamente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al eliminar el stock.", error = ex.Message });
            }
        }
    }
}
