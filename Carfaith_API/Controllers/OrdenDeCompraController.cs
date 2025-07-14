using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrdenDeCompraController : Controller
    {
        private readonly IOrdenDeCompraServicio _ordenDeCompraServicio;

        public OrdenDeCompraController(IOrdenDeCompraServicio ordenDeCompraServicio)
        {
            _ordenDeCompraServicio = ordenDeCompraServicio;
        }

        [HttpPost("crearOrdenDeCompra")]
        public async Task<IActionResult> CrearOrdenDeCompra([FromBody] OrdenDeCompra ordenDeCompra)
        {
            try
            {
                await _ordenDeCompraServicio.AddOrdenDeCompraAsync(ordenDeCompra);
                return Ok(ordenDeCompra);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }
    }
}
