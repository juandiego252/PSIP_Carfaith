using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class ProductoServicioImpl : IProductoServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductoServicioImpl(CarfaithDbContext _context)
        {
            _productoRepositorio = new ProductoRepositorioImpl(_context);
        }

        public async Task AddProductoAsync(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo.");
            }
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                throw new ArgumentException("El nombre del producto no puede ser nulo o vacío.", nameof(producto));
            }
            if (producto.LineaDeProducto == null)
            {
                throw new ArgumentException("La línea de producto no puede ser nula.", nameof(producto));
            }

            if (string.IsNullOrEmpty(producto.CodigoProducto))
            {
                producto.CodigoProducto = await GenerarCodigoProductoAsync();
            }


            if (!string.IsNullOrEmpty(producto.CodigoProducto) && !await _productoRepositorio.IsCodigoProductoUnique(producto.CodigoProducto))
            {
                throw new ArgumentException($"El código de producto {producto.CodigoProducto} ya se encuentra registrado", nameof(producto));
            }
            await _productoRepositorio.AddAsync(producto);
        }
        private async Task<string> GenerarCodigoProductoAsync()
        {
            // Obtener todos los productos para encontrar el último código
            var productos = await _productoRepositorio.GetAllAsync();

            // Filtrar solo los códigos con formato PROD-XXXX
            var codigosExistentes = productos
                .Where(p => p.CodigoProducto != null && p.CodigoProducto.StartsWith("PROD-"))
                .Select(p => p.CodigoProducto)
                .ToList();

            // Encontrar el número más alto actual
            int maxNumero = 0;
            foreach (var codigo in codigosExistentes)
            {
                if (int.TryParse(codigo.Substring(5), out int numero))
                {
                    if (numero > maxNumero)
                        maxNumero = numero;
                }
            }

            // Generar el siguiente número
            int siguienteNumero = maxNumero + 1;

            // Formatear el nuevo código con ceros a la izquierda (PROD-0001)
            return $"PROD-{siguienteNumero:D4}";
        }

        public async Task DeleteProductoByIdAsync(int id)
        {
            await _productoRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<Producto>> GetAllProductoAsync()
        {
            return await _productoRepositorio.GetAllAsync();
        }

        public async Task<Producto> GetByIdProductoAsync(int id)
        {
            return await _productoRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            var productoExistente = await _productoRepositorio.GetByIdAsync(producto.IdProducto);
            if (productoExistente == null)
            {
                throw new KeyNotFoundException($"Producto con ID {producto.IdProducto} no encontrado.");
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.CodigoProducto = producto.CodigoProducto;
            productoExistente.LineaDeProducto = producto.LineaDeProducto;

            await _productoRepositorio.UpdateAsync(productoExistente);

        }

        public async Task<IEnumerable<ProductoResumenDTO>> GetProductosResumen()
        {
            return await _productoRepositorio.GetProductosResumen();
        }
    }
}
