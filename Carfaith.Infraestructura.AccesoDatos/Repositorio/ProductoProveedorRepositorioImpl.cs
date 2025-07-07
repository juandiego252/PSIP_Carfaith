using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class ProductoProveedorRepositorioImpl : RepositorioImpl<ProductoProveedor>, IProductoProveedorRepositorio
    {
        private readonly CarfaithDbContext _context;
        public ProductoProveedorRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductoProveedorDetalleAsync()
        {
            var query = from productoProveedor in _context.ProductoProveedors
                        join producto in _context.Productos on productoProveedor.IdProducto equals producto.IdProducto
                        join proveedor in _context.Proveedores on productoProveedor.IdProveedor equals proveedor.IdProveedor
                        join lineasProducto in _context.LineasDeProductos on producto.LineaDeProducto equals lineasProducto.IdLinea
                        select new ProductoProveedorDetalleDTO
                        {
                            IdProductoProveedor = productoProveedor.IdProductoProveedor,
                            NombreProducto = producto.Nombre!,
                            CodigoProducto = producto.CodigoProducto!,
                            NombreProveedor = proveedor.NombreProveedor!,
                            PaisOrigen = proveedor.PaisOrigen!,
                            LineaProducto = lineasProducto.Nombre!
                        };
            return await query.ToListAsync();
        }
    }
}
