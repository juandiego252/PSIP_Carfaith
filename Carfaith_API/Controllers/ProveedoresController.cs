using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ProveedoresController : ControllerBase
    {
        private IProveedoresServicio _proveedorServicio;

        public ProveedoresController(IProveedoresServicio proveedorServicio)
        {
            _proveedorServicio = proveedorServicio;
        }

        [HttpPost("CrearProveedor")]
        public async Task<IActionResult> CrearProveedor([FromBody] Proveedores proveedor)
        {
            try
            {
                await _proveedorServicio.AddProveedoresAsync(proveedor);

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.ToString());

                return StatusCode(500, "Error Interno");
            }
        }

        [HttpGet("ListarProveedores")]
        public async Task<IActionResult> GetProveedores()
        {
            try
            {
                var proveedores = await _proveedorServicio.GetAllProveedoresAsync();

                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.ToString());

                return StatusCode(500, "Error Interno");
            }
        }

        [HttpGet("ListarProveedores/{id}")]
        public async Task<IActionResult> GetProveedoresById(int id)
        {
            try
            {
                var proveedores = await _proveedorServicio.GetByIdProveedoresAsync(id);

                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.ToString());

                return StatusCode(500, "Error Interno");
            }
        }

        [HttpPut("ActualizarProveedor")]
        public async Task<IActionResult> ActualizarProveedor([FromBody] Proveedores proveedor)
        {
            try
            {
                await _proveedorServicio.UpdateProveedoresAsync(proveedor);

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.ToString());

                return StatusCode(500, "Error Interno");
            }
        }

        [HttpDelete("EliminarProveedor/{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            try
            {
                await _proveedorServicio.DeleteProveedoresByIdAsync(id);
                return Ok($"Proveedor con id {id} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error: " + ex.ToString());
                return StatusCode(500, "Error interno");
            }
        }
    }
}
