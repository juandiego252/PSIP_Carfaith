using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenIngreso;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class OrdenIngresoStockServicioImpl : IOrdenIngresoStockServicio
    {
        private readonly CarfaithDbContext _context;
        private readonly IOrdenDeIngresoServicio _ordenDeIngresoServicio;
        private readonly IDetalleOrdenIngresoServicio _detalleOrdenIngresoServicio;
        private readonly IStockServicio _stockServicio;

        public OrdenIngresoStockServicioImpl(CarfaithDbContext context, IOrdenDeIngresoServicio ordenIngresoServicio,
        IDetalleOrdenIngresoServicio detalleOrdenIngresoServicio,
        IStockServicio stockServicio)
        {
            _context = context;
            _ordenDeIngresoServicio = ordenIngresoServicio;
            _detalleOrdenIngresoServicio = detalleOrdenIngresoServicio;
            _stockServicio = stockServicio;
        }
        public async Task<int> CrearOrdenIngresoConDetalles(OrdenIngresoConDetallesDTO ordenIngresoConDetallesDTO)
        {
            if (ordenIngresoConDetallesDTO == null)
            {
                throw new ArgumentNullException(nameof(ordenIngresoConDetallesDTO), "El DTO de orden de ingreso no puede ser nulo.");
            }
            if (ordenIngresoConDetallesDTO.Detalles == null || !ordenIngresoConDetallesDTO.Detalles.Any())
            {
                throw new ArgumentException("La orden de ingreso debe tener al menos un detalle");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // 1. Crear la orden de ingreso
                    var ordenIngreso = new OrdenDeIngreso
                    {
                        IdOrdenCompra = ordenIngresoConDetallesDTO.IdOrdenCompra,
                        OrigenDeCompra = ordenIngresoConDetallesDTO.OrigenDeCompra,
                        Fecha = ordenIngresoConDetallesDTO.Fecha,
                        Estado = ordenIngresoConDetallesDTO.Estado
                    };

                    await _ordenDeIngresoServicio.AddOrdenDeIngresoAsync(ordenIngreso);

                    // 2. Crear los detalles de la orden de ingreso y actualizar el stock

                    foreach (var detalleDTO in ordenIngresoConDetallesDTO.Detalles)
                    {
                        var detalle = new DetalleOrdenIngreso
                        {
                            OrdenIngresoId = ordenIngreso.IdOrdenIngreso,
                            IdProductoProveedor = detalleDTO.IdProductoProveedor,
                            Cantidad = detalleDTO.Cantidad,
                            PrecioUnitario = detalleDTO.PrecioUnitario,
                            UbicacionId = detalleDTO.UbicacionId,
                            TipoIngreso = detalleDTO.TipoIngreso,
                            NumeroLote = detalleDTO.NumeroLote
                        };

                        await _detalleOrdenIngresoServicio.AddDetalleOrdenIngresoAsync(detalle);

                        // Actualizar el stock
                        await _stockServicio.ActualizarStockAsync(detalle.IdProductoProveedor.Value, detalle.UbicacionId.Value, detalle.Cantidad.Value);
                    }

                    scope.Complete();
                    return ordenIngreso.IdOrdenIngreso;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
