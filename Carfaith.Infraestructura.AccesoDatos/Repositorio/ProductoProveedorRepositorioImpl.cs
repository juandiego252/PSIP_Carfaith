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

        public async Task<IEnumerable<ProductoPorLineaProveedorDTO>> GetProductosPorLineaProveedorAsync()
        {
            var query = from linea in _context.LineasDeProductos
                        select new ProductoPorLineaProveedorDTO
                        {
                            NombreLinea = linea.Nombre!,
                            TotalProductos = (from producto in _context.Productos
                                              where producto.LineaDeProducto == linea.IdLinea
                                              select producto).Count(),
                            TotalProveedores = (from producto in _context.Productos
                                                where producto.LineaDeProducto == linea.IdLinea
                                                join productoProveedor in _context.ProductoProveedors
                                                on producto.IdProducto equals productoProveedor.IdProducto
                                                select productoProveedor.IdProveedor).Distinct().Count(),
                            ProductosProveedores = (from producto in _context.Productos
                                                    where producto.LineaDeProducto == linea.IdLinea
                                                    join productoProveedor in _context.ProductoProveedors
                                                    on producto.IdProducto equals productoProveedor.IdProducto
                                                    join proveedor in _context.Proveedores
                                                    on productoProveedor.IdProveedor equals proveedor.IdProveedor
                                                    select new ProductoProveedorBasicoDTO
                                                    {
                                                        IdProductoProveedor = productoProveedor.IdProductoProveedor,
                                                        NombreProducto = producto.Nombre!,
                                                        NombreProveedor = proveedor.NombreProveedor!
                                                    }).ToList()
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductosProveedoresPorLineaAsync(int idLinea)
        {
            var query = from productoProveedor in _context.ProductoProveedors
                        join producto in _context.Productos on productoProveedor.IdProducto equals producto.IdProducto
                        join proveedor in _context.Proveedores on productoProveedor.IdProveedor equals proveedor.IdProveedor
                        join lineasProducto in _context.LineasDeProductos on producto.LineaDeProducto equals lineasProducto.IdLinea
                        where lineasProducto.IdLinea == idLinea
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

        public async Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductosProveedoresPorPaisAsync(string paisOrigen)
        {
            var query = from productoProveedor in _context.ProductoProveedors
                        join producto in _context.Productos on productoProveedor.IdProducto equals producto.IdProducto
                        join proveedor in _context.Proveedores on productoProveedor.IdProveedor equals proveedor.IdProveedor
                        join lineasProducto in _context.LineasDeProductos on producto.LineaDeProducto equals lineasProducto.IdLinea
                        where proveedor.PaisOrigen == paisOrigen
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

        public async Task<IEnumerable<ProveedorProductosDTO>> GetProveedoresConProductosAsync()
        {
            var query = from proveedor in _context.Proveedores
                        select new ProveedorProductosDTO
                        {
                            IdProveedor = proveedor.IdProveedor,
                            NombreProveedor = proveedor.NombreProveedor!,
                            PaisOrigen = proveedor.PaisOrigen!,
                            Ruc = proveedor.Ruc!,
                            TotalProductos = proveedor.ProductoProveedors.Count(),
                            Productos = (from productosProveedor in proveedor.ProductoProveedors
                                         join producto in _context.Productos on productosProveedor.IdProducto equals producto.IdProducto
                                         join lineasProducto in _context.LineasDeProductos on producto.LineaDeProducto equals lineasProducto.IdLinea
                                         select new ProductoResumenDTO
                                         {
                                             IdProducto = producto.IdProducto,
                                             Nombre = producto.Nombre!,
                                             CodigoProducto = producto.CodigoProducto!,
                                             LineaProducto = lineasProducto.Nombre!,
                                         }).ToList()
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<LineaProductoProveedoresDTO>> GetLineasConProveedoresAsync()
        {
            var query = from lineaProductos in _context.LineasDeProductos
                        select new LineaProductoProveedoresDTO
                        {
                            IdLinea = lineaProductos.IdLinea,
                            NombreLinea = lineaProductos.Nombre!,
                            TotalProductos = (from producto in _context.Productos
                                              where producto.LineaDeProducto == lineaProductos.IdLinea
                                              select producto).Count(),
                            TotalProveedores = (from producto in _context.Productos
                                                where producto.LineaDeProducto == lineaProductos.IdLinea
                                                join productoProveedor in _context.ProductoProveedors on producto.IdProducto equals productoProveedor.IdProducto
                                                select productoProveedor.IdProveedor).Distinct().Count(),
                            Proveedores = (from producto in _context.Productos
                                           where producto.LineaDeProducto == lineaProductos.IdLinea
                                           join productoProveedor in _context.ProductoProveedors
                                           on producto.IdProducto equals productoProveedor.IdProducto
                                           join proveedor in _context.Proveedores
                                           on productoProveedor.IdProveedor equals proveedor.IdProveedor
                                           select new ProveedorResumenDTO
                                           {
                                               IdProveedor = proveedor.IdProveedor,
                                               NombreProveedor = proveedor.NombreProveedor!,
                                               PaisOrigen = proveedor.PaisOrigen!,
                                           }).Distinct().ToList()
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ProveedorProductosPorPaisDTO>> GetProveedorProductosPorPaisAsync()
        {
            var query = from proveedor in _context.Proveedores
                        group proveedor by proveedor.PaisOrigen into paisGroup
                        select new ProveedorProductosPorPaisDTO
                        {
                            PaisOrigen = paisGroup.Key!,
                            TotalProveedores = paisGroup.Count(),
                            Proveedores = (from p in paisGroup
                                           select new ProveedorConProductosDTO
                                           {
                                               IdProveedor = p.IdProveedor,
                                               NombreProveedor = p.NombreProveedor!,
                                               TotalProductos = p.ProductoProveedors.Count
                                           }).ToList()
                        };

            return await query.ToListAsync();
        }
    }
}
