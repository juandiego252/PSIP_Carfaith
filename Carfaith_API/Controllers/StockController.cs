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
                return Ok(new { message = "Stock creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear el stock.", error = ex.Message });
            }
        }

        [HttpGet("ListarStockDetalle")]

        public async Task<IActionResult> ListarStock()
        {
            try
            {
                var stockList = await _stockServicio.GetStockProductoProveedorUbicacionDTO();
                return Ok(stockList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al obtener el stock.", error = ex.Message });
            }
        }
    }
}
