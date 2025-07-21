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
    public class ProductoRepositorioImpl : RepositorioImpl<Producto>, IProductoRepositorio
    {
        private readonly CarfaithDbContext _context;
        public ProductoRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoResumenDTO>> GetProductosResumen()
        {
            var query = from p in _context.Productos
                        join lineaProducto in _context.LineasDeProductos on p.LineaDeProducto equals lineaProducto.IdLinea
                        select new ProductoResumenDTO
                        {
                            IdProducto = p.IdProducto,
                            NombreProducto = p.Nombre,
                            CodigoProducto = p.CodigoProducto,
                            IdLineaProdcuto = lineaProducto.IdLinea,
                            NombreLineaProducto = lineaProducto.Nombre
                        };

            return await query.ToListAsync();
        }

        public async Task<bool> IsCodigoProductoUnique(string codigoProducto)
        {
            try
            {
                var codigoProductoExists = from p in _context.Productos
                                           where p.CodigoProducto == codigoProducto
                                           select p;

                return await codigoProductoExists.AnyAsync() == false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la unicidad del código de producto: " + ex.Message);
            }
        }
    }
}
