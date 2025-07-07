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
            if (!string.IsNullOrEmpty(producto.CodigoProducto) && !await _productoRepositorio.IsCodigoProductoUnique(producto.CodigoProducto))
            {
                throw new ArgumentException($"El código de producto {producto.CodigoProducto} ya se encuentra registrado", nameof(producto));
            }
            await _productoRepositorio.AddAsync(producto);
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
            await _productoRepositorio.UpdateAsync(producto);
        }
    }
}
