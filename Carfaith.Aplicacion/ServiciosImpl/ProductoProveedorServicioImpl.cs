using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class ProductoProveedorServicioImpl : IProductoProveedorServicio
    {
        private readonly IProductoProveedorRepositorio _productoProveedorRepositorio;

        public ProductoProveedorServicioImpl(CarfaithDbContext _context)
        {
            _productoProveedorRepositorio = new ProductoProveedorRepositorioImpl(_context);
        }

        // CRUD de ProductoProveedor
        public async Task AddProductoProveedorAsync(ProductoProveedor productoProveedor)
        {
            if (productoProveedor == null)
            {
                throw new ArgumentNullException(nameof(productoProveedor), "El producto proveedor no puede ser nulo.");
            }
            if (productoProveedor.IdProducto == null)
            {
                throw new ArgumentException("El ID del producto no puede ser nulo.", nameof(productoProveedor));
            }
            if (productoProveedor.IdProveedor == null)
            {
                throw new ArgumentException("El ID del proveedor no puede ser nulo.", nameof(productoProveedor));
            }

            await _productoProveedorRepositorio.AddAsync(productoProveedor);
        }

        public async Task DeleteProductoProveedorAsync(int idProductoProveedor)
        {
            await _productoProveedorRepositorio.DeleteAsync(idProductoProveedor);
        }

        public async Task<IEnumerable<ProductoProveedor>> GetAllProductoProveedorAsync()
        {
            return await _productoProveedorRepositorio.GetAllAsync();
        }

        public Task<ProductoProveedor> GetByIdProductoProveedorAsync(int idProductoProveedor)
        {
            return _productoProveedorRepositorio.GetByIdAsync(idProductoProveedor);
        }


        public async Task UpdateProductoProveedorAsync(ProductoProveedor productoProveedor)
        {
            await _productoProveedorRepositorio.UpdateAsync(productoProveedor);
        }

        // Consultas
        public async Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductoProveedorDetallesAsync()
        {
            return await _productoProveedorRepositorio.GetProductoProveedorDetalleAsync();
        }

        public async Task<IEnumerable<ProveedorProductosDTO>> GetProveedoresConProductosAsync()
        {
            return await _productoProveedorRepositorio.GetProveedoresConProductosAsync();
        }

        public async Task<IEnumerable<LineaProductoProveedoresDTO>> GetLineasConProveedoresAsync()
        {
             return await _productoProveedorRepositorio.GetLineasConProveedoresAsync();
        }

        public async Task<IEnumerable<ProductoPorLineaProveedorDTO>> GetProductosPorLineaProveedorAsync()
        {
            return await _productoProveedorRepositorio.GetProductosPorLineaProveedorAsync();
        }

        public async Task<IEnumerable<ProveedorProductosPorPaisDTO>> GetProveedorProductosPorPaisAsync()
        {
            return await _productoProveedorRepositorio.GetProveedorProductosPorPaisAsync();
        }

        public async Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductosProveedoresPorLineaAsync(int idLinea)
        {
            return await _productoProveedorRepositorio.GetProductosProveedoresPorLineaAsync(idLinea);
        }

        public async Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductosProveedoresPorPaisAsync(string paisOrigen)
        {
            return await _productoProveedorRepositorio.GetProductosProveedoresPorPaisAsync(paisOrigen);
        }
    }
}
