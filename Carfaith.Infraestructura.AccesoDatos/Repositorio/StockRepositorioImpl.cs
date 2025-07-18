using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class StockRepositorioImpl : RepositorioImpl<Stock>, IStockRepositorio
    {
        CarfaithDbContext _context;
        public StockRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Stock> ActualizarStockPorTransferenciaAsync(int idProductoProveedor, int idUbicacion, int cantidad)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.IdProductoProveedor == idProductoProveedor && s.IdUbicacion == idUbicacion);

            if (stock != null)
            {
                stock.Cantidad = (stock.Cantidad ?? 0) + cantidad;
                await _context.SaveChangesAsync();
            }
            else
            {
                stock = new Stock
                {
                    IdProductoProveedor = idProductoProveedor,
                    IdUbicacion = idUbicacion,
                    Cantidad = cantidad > 0 ? cantidad : 0
                };
                _context.Stocks.Add(stock);
                await _context.SaveChangesAsync();
            }

            return stock;

        }

        public async Task<Stock> GetStockByProductoProveedorYUbicacionAsync(int idProductoProveedor, int idUbicacion)
        {
            var query = (from stock in _context.Stocks
                         where stock.IdProductoProveedor == idProductoProveedor && stock.IdUbicacion == idUbicacion
                         select stock).FirstOrDefaultAsync();

            return await query;
        }

        public async Task<IEnumerable<StockProductoProveedorUbicacionDTO>> GetStockProductoProveedorUbicacionDto()
        {
            var query = from stock in _context.Stocks
                        join productoProveedor in _context.ProductoProveedors on stock.IdProductoProveedor equals productoProveedor.IdProductoProveedor
                        join producto in _context.Productos on productoProveedor.IdProducto equals producto.IdProducto
                        join proveedor in _context.Proveedores on productoProveedor.IdProveedor equals proveedor.IdProveedor
                        join ubicacion in _context.Ubicaciones on stock.IdUbicacion equals ubicacion.IdUbicacion
                        select new StockProductoProveedorUbicacionDTO
                        {
                            IdStock = stock.IdStock,
                            Cantidad = stock.Cantidad,
                            CodigoProducto = producto.CodigoProducto,
                            NombreProducto = producto.Nombre,
                            NombreProveedor = proveedor.NombreProveedor,
                            TipoProveedor = proveedor.TipoProveedor,
                            LugarUbicacion = ubicacion.LugarUbicacion
                        };
            return await query.ToListAsync();
        }
    }
}
