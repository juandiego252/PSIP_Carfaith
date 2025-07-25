﻿using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoProveedorController : ControllerBase
    {
        private readonly IProductoProveedorServicio _productoProveedorServicio;

        public ProductoProveedorController(IProductoProveedorServicio productoProveedorServicio)
        {
            _productoProveedorServicio = productoProveedorServicio;
        }

        [HttpPost("AsociarProductoProveedor")]
        public async Task<IActionResult> AsociarProductoProveedor([FromBody] ProductoProveedor productoProveedor)
        {
            try
            {
                await _productoProveedorServicio.AddProductoProveedorAsync(productoProveedor);
                return Ok(new { message = "ProductoProveedor asociado correctamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al asociar" + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al asociar ProductoProveedor." });

            }
        }
        [HttpPost("AsociarMasivaProductoProveedores")]
        public async Task<IActionResult> AsociarProductoConProveedores([FromBody] AsociacionMasivaDTO asociacionDTO)
        {
            try
            {
                var productosProveedores = await _productoProveedorServicio.AsociarProductoConProveedoresAsync(asociacionDTO);

                return Ok(new
                {
                    message = "Proveedores asociados correctamente al producto.",
                    totalAsociados = productosProveedores.Count
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al asociar proveedores al producto: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al asociar proveedores al producto: " + ex.Message });
            }
        }


        [HttpGet("ListarDetalleProductoProveedor")]
        public async Task<IActionResult> ListarDetalleProductoProveedor()
        {
            try
            {
                var detalles = await _productoProveedorServicio.GetProductoProveedorDetallesAsync();
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar detalles de ProductoProveedor: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al listar detalles de ProductoProveedor." });
            }
        }

        [HttpPut("ActualizarProductoProveedor")]
        public async Task<IActionResult> ActualizarProductoProveedor([FromBody] ProductoProveedor productoProveedor)
        {
            try
            {
                await _productoProveedorServicio.UpdateProductoProveedorAsync(productoProveedor);
                return Ok(new { message = "ProductoProveedor actualizado correctamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar ProductoProveedor: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al actualizar ProductoProveedor: " + ex.Message });
            }
        }


        [HttpDelete("EliminarProductoProveedor/{id}")]
        public async Task<IActionResult> EliminarProductoProveedor(int id)
        {
            try
            {
                await _productoProveedorServicio.DeleteProductoProveedorAsync(id);
                return Ok(new { message = "ProductoProveedor eliminado correctamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar ProductoProveedor: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al eliminar ProductoProveedor." });
            }
        }

    }
}
