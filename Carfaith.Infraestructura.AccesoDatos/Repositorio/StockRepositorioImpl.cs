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
