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
    public class OrdenDeIngresoRepositorioImpl : RepositorioImpl<OrdenDeIngreso>, IOrdenDeIngresoRepositorio
    {
        private readonly CarfaithDbContext _context;
        public OrdenDeIngresoRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdenDeIngresoOrdenCompraDTO>> GetOrdenDeIngresoOrdenCompraDTOs()
        {
            var query = from ordenIngreso in _context.OrdenDeIngresos
                        join ordenCompra in _context.OrdenDeCompras
                        on ordenIngreso.IdOrdenCompra equals ordenCompra.IdOrden into ocGroup
                        from ordenCompra in ocGroup.DefaultIfEmpty() // Left join para incluir órdenes sin compra asociada
                        join proveedor in _context.Proveedores
                        on ordenCompra.IdProveedor equals proveedor.IdProveedor into provGroup
                        from proveedor in provGroup.DefaultIfEmpty() // Left join para incluir órdenes sin proveedor
                        select new OrdenDeIngresoOrdenCompraDTO
                        {
                            // Propiedades de Orden de Ingreso
                            IdOrdenIngreso = ordenIngreso.IdOrdenIngreso,
                            OrigenDeCompra = ordenIngreso.OrigenDeCompra,

                            // Propiedades de Orden de Compra
                            NumeroOrdenCompra = ordenCompra.NumeroOrden,
                            ArchivoPdf = ordenCompra.ArchivoPdf,

                            // Propiedades del Proveedor
                            NombreProveedor = proveedor.NombreProveedor,
                            PaisOrigen = proveedor.PaisOrigen,
                            TipoProveedor = proveedor.TipoProveedor,
                            PersonaContacto = proveedor.PersonaContacto
                        };

            return await query.ToListAsync();
        }
    }
}
