using Carfaith.Aplicacion.DTO;
using Carfaith.Aplicacion.DTO.DTOs.OrdenDeCompra;
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
    public class PreciosHistoricosRepositorioImpl : RepositorioImpl<PreciosHistoricos>, IPreciosHistoricosRepositorio
    {
        private readonly CarfaithDbContext _context;

        public PreciosHistoricosRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PreciosHistoricosDTO>> GetPreciosHistoricosProductos()
        {
            try
            {
                var result = from ph in _context.PreciosHistoricos
                             join productProveedor in _context.ProductoProveedors on ph.IdProductoProveedor equals productProveedor.IdProductoProveedor
                             join prov in _context.Proveedores on productProveedor.IdProveedor equals prov.IdProveedor
                             join prod in _context.Productos on productProveedor.IdProducto equals prod.IdProducto
                             join lineProduct in _context.LineasDeProductos on prod.LineaDeProducto equals lineProduct.IdLinea
                             select new PreciosHistoricosDTO
                             {
                                 IdPreciosHistoricos = ph.IdPreciosHistoricos,
                                 CodigoProducto = prod.CodigoProducto,
                                 NombreProducto = prod.Nombre,
                                 LineaProducto = lineProduct.Nombre,
                                 NombreProveedor = prov.NombreProveedor,
                                 TipoProveedor = prov.TipoProveedor,
                                 Precio = ph.Precio,
                                 FechaInicio = ph.FechaInicio,
                                 FechaFinalizacion = ph.FechaFinalizacion
                             };

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: No se pudieron obtener los precios historicos, " + ex.Message);
            }
        }
    }
}
