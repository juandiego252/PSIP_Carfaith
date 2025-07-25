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
    public class OrdenDeCompraRepositorioImpl : RepositorioImpl<OrdenDeCompra>, IOrdenDeCompraRepositorio
    {
        private readonly CarfaithDbContext _context;
        public OrdenDeCompraRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<OrdenDeCompraInfoDTO>> GetOrdenesDeCompraProveedor()
        {
            try
            {
                var result = from od in _context.OrdenDeCompras
                             join p in _context.Proveedores
                             on od.IdProveedor equals p.IdProveedor
                             select new OrdenDeCompraInfoDTO
                             {
                                 idOrden = od.IdOrden,
                                 numeroOrden = od.NumeroOrden,
                                 idProveedor = p.IdProveedor,
                                 nombreProveedor = p.NombreProveedor,
                                 archivoPdf = od.ArchivoPdf,
                                 estado = od.Estado,
                                 fechaCreacion = od.FechaCreacion,
                                 fechaEstimadaEntrega = od.FechaEstimadaEntrega
                             };

                return result.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: No se pudieron obtener las ordenes de compra, " + ex.Message);
            }
        }

        public async Task<bool> IsCodigoOrdenUnique(string codigoOrden)
        {
            try
            {
                var codigoOrdenExists = from oc in _context.OrdenDeCompras
                                        where oc.NumeroOrden == codigoOrden
                                        select oc;

                return await codigoOrdenExists.AnyAsync() == false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la unicidad del número de orden de la Orden de Compra: " + ex.Message);
            }
        }
    }
}
