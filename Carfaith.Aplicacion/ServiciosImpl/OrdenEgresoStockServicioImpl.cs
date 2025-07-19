using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class OrdenEgresoStockServicioImpl : IOrdenEgresoStockServicio
    {
        private readonly CarfaithDbContext _context;
        private readonly IOrdenEgresoServicio _ordenEgresoServicio;
        private readonly IDetalleOrdenEgresoServicio _detalleOrdenEgresoServicio;
        private readonly IStockServicio _stockServicio;

        public OrdenEgresoStockServicioImpl(CarfaithDbContext context,
            IOrdenEgresoServicio ordenEgresoServicio,
            IDetalleOrdenEgresoServicio detalleOrdenEgresoServicio,
            IStockServicio stockServicio)
        {
            _context = context;
            _ordenEgresoServicio = ordenEgresoServicio;
            _detalleOrdenEgresoServicio = detalleOrdenEgresoServicio;
            _stockServicio = stockServicio;
        }
        public async Task<int> CrearOrdenEgresoConDetalles(OrdenEgresoConDetallesDTO ordenEgresoConDetallesDTO)
        {
            if (ordenEgresoConDetallesDTO == null)
            {
                throw new ArgumentNullException(nameof(ordenEgresoConDetallesDTO), "El DTO de orden de egreso no puede ser nulo.");
            }

            if (ordenEgresoConDetallesDTO.Detalles == null || !ordenEgresoConDetallesDTO.Detalles.Any())
            {
                throw new ArgumentException("La orden de egreso debe tener al menos un detalle");
            }

            foreach (var detalle in ordenEgresoConDetallesDTO.Detalles)
            {
                if (!detalle.IdProductoProveedor.HasValue || !detalle.UbicacionId.HasValue || !detalle.Cantidad.HasValue)
                {
                    throw new ArgumentException("Todos los detalles deben especificar producto, ubicacion y cantidad.");
                }

                var stockActual = await _stockServicio.GetStockByProductoProveedorYUbicacionAsync(
                    detalle.IdProductoProveedor.Value, detalle.UbicacionId.Value);

                if (stockActual == null || stockActual.Cantidad < detalle.Cantidad)
                {
                    throw new InvalidOperationException(
                            $"No hay suficiente stock para el producto {detalle.IdProductoProveedor} " +
                            $"en la ubicación {detalle.UbicacionId}. " +
                            $"Disponible: {stockActual?.Cantidad ?? 0}, Solicitado: {detalle.Cantidad}");
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    // 1. Crear la orden de egreso

                    var ordenEgreso = new OrdenEgreso
                    {
                        TipoEgreso = ordenEgresoConDetallesDTO.TipoEgreso,
                        Fecha = ordenEgresoConDetallesDTO.Fecha ?? DateOnly.FromDateTime(DateTime.Now),
                        Destino = ordenEgresoConDetallesDTO.Destino,
                        Estado = ordenEgresoConDetallesDTO.Estado ?? "Pendiente"
                    };

                    await _ordenEgresoServicio.AddOrdenEgresoAsync(ordenEgreso);

                    // 2. Crear los detalles de la orden de egreso y reducir el stock

                    foreach (var detalleDTO in ordenEgresoConDetallesDTO.Detalles)
                    {
                        var detalle = new DetalleOrdenEgreso
                        {
                            OrdenEgresoId = ordenEgreso.IdOrdenEgreso,
                            IdProductoProveedor = detalleDTO.IdProductoProveedor,
                            Cantidad = detalleDTO.Cantidad,
                            UbicacionId = detalleDTO.UbicacionId
                        };

                        await _detalleOrdenEgresoServicio.AddDetalleOrdenEgreso(detalle);

                        // 3. Reducir el stock
                        await _stockServicio.ActualizarStockAsync(
                            detalleDTO.IdProductoProveedor.Value,
                            detalleDTO.UbicacionId.Value,
                            -detalleDTO.Cantidad.Value);
                    }

                    scope.Complete();
                    return ordenEgreso.IdOrdenEgreso;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
