using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenIngreso;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;
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
                    var ordenEgreso = new OrdenEgreso
                    {
                        Fecha = ordenEgresoConDetallesDTO.Fecha ?? DateOnly.FromDateTime(DateTime.Now),
                        Destino = ordenEgresoConDetallesDTO.Destino,
                        Estado = ordenEgresoConDetallesDTO.Estado
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
                            TipoEgreso = detalleDTO.TipoEgreso,
                            UbicacionId = detalleDTO.UbicacionId
                        };

                        await _detalleOrdenEgresoServicio.AddDetalleOrdenEgreso(detalle);

                        // 3. Reducir el stock
                        await _stockServicio.ActualizarStockAsync(
                            detalle.IdProductoProveedor.Value,
                            detalle.UbicacionId.Value,
                            detalle.Cantidad.Value);
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

        public async Task<int> EditarOrdenEgresoConDetalles(OrdenEgresoConDetallesDTO ordenEgresoConDetallesDTO)
        {
            if (ordenEgresoConDetallesDTO == null)
            {
                throw new ArgumentNullException(nameof(ordenEgresoConDetallesDTO), "El DTO de orden de egreso no puede ser nulo.");
            }

            if (ordenEgresoConDetallesDTO.Detalles == null || !ordenEgresoConDetallesDTO.Detalles.Any())
            {
                throw new ArgumentException("La orden de egreso debe tener al menos un detalle");
            }

            var ordenEgresoExistente = await _ordenEgresoServicio.GetOrdenEgresoByIdAsync(ordenEgresoConDetallesDTO.IdOrdenEgreso.Value);
            if (ordenEgresoExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró una orden de ingreso con el ID {ordenEgresoConDetallesDTO.IdOrdenEgreso}");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    ordenEgresoExistente.Fecha = ordenEgresoConDetallesDTO.Fecha;
                    ordenEgresoExistente.Destino = ordenEgresoConDetallesDTO.Destino;
                    ordenEgresoExistente.Estado = ordenEgresoConDetallesDTO.Estado;

                    await _ordenEgresoServicio.UpdateOrdenEgresoAsync(ordenEgresoExistente);

                    var detallesActuales = await _context.DetalleOrdenEgresos
                        .Where(d => d.OrdenEgresoId == ordenEgresoExistente.IdOrdenEgreso)
                        .ToListAsync();

                    foreach (var detalle in detallesActuales)
                    {
                        if (detalle.IdProductoProveedor.HasValue && detalle.UbicacionId.HasValue && detalle.Cantidad.HasValue)
                        {
                            await _stockServicio.ActualizarStockAsync(
                                detalle.IdProductoProveedor.Value,
                                detalle.UbicacionId.Value,
                                -detalle.Cantidad.Value);
                        }
                    }

                    foreach (var detalle in detallesActuales)
                    {
                        await _detalleOrdenEgresoServicio.DeleteDetalleOrdenEgreso(detalle.IdDetalleOrdenEgreso);
                    }

                    foreach (var detalleDTO in ordenEgresoConDetallesDTO.Detalles)
                    {
                        var nuevoDetalle = new DetalleOrdenEgreso
                        {
                            OrdenEgresoId = ordenEgresoExistente.IdOrdenEgreso,
                            IdProductoProveedor = detalleDTO.IdProductoProveedor,
                            Cantidad = detalleDTO.Cantidad,
                            TipoEgreso = detalleDTO.TipoEgreso,
                            UbicacionId = detalleDTO.UbicacionId
                        };

                        await _detalleOrdenEgresoServicio.AddDetalleOrdenEgreso(nuevoDetalle);

                        if (nuevoDetalle.IdProductoProveedor.HasValue && nuevoDetalle.UbicacionId.HasValue && nuevoDetalle.Cantidad.HasValue)
                        {
                            await _stockServicio.ActualizarStockAsync(
                                nuevoDetalle.IdProductoProveedor.Value,
                                nuevoDetalle.UbicacionId.Value,
                                nuevoDetalle.Cantidad.Value);
                        }
                    }

                    scope.Complete();
                    return ordenEgresoExistente.IdOrdenEgreso;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task EliminarOrdenEgresoConDetalles(int idOrdenEgreso)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // 1. Obtener la orden y sus detalles
                    var orden = await _ordenEgresoServicio.GetOrdenEgresoByIdAsync(idOrdenEgreso);
                    if (orden == null)
                    {
                        throw new KeyNotFoundException($"No se encontró la orden de egreso con ID {idOrdenEgreso}");
                    }

                    // 2. Obtener los detalles para actualizar el stock
                    var detalles = await _context.DetalleOrdenEgresos
                        .Where(d => d.OrdenEgresoId == idOrdenEgreso)
                        .ToListAsync();

                    // 3. Actualizar el stock (restar los productos ingresados)
                    foreach (var detalle in detalles)
                    {
                        if (detalle.IdProductoProveedor.HasValue && detalle.UbicacionId.HasValue && detalle.Cantidad.HasValue)
                        {
                            await _stockServicio.ActualizarStockAsync(
                                detalle.IdProductoProveedor.Value,
                                detalle.UbicacionId.Value,
                                -detalle.Cantidad.Value); // Restamos la cantidad que se había ingresado
                        }
                    }

                    // 4. Eliminar cada detalle manualmente
                    foreach (var detalle in detalles)
                    {
                        await _detalleOrdenEgresoServicio.DeleteDetalleOrdenEgreso(detalle.IdDetalleOrdenEgreso);
                    }

                    // 5. Eliminar la orden de ingreso
                    await _ordenEgresoServicio.DeleteOrdenEgresoAsync(idOrdenEgreso);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, la transacción se revierte automáticamente
                    throw new Exception($"Error al eliminar la orden de ingreso: {ex.Message}", ex);
                }
            }
        }

        public async Task<IEnumerable<OrdenEgresoConDetallesDTO>> GetOrdenEgresoConDetalles()
        {
            var ordenesIngreso = await _ordenEgresoServicio.GetAllOrdenEgresosAsync();
            var result = new List<OrdenEgresoConDetallesDTO>();

            foreach (var ordenIngreso in ordenesIngreso)
            {
                var detalles = await _context.DetalleOrdenEgresos.Where(d => d.OrdenEgresoId == ordenIngreso.IdOrdenEgreso).ToListAsync();

                var ordenEgresoDTO = new OrdenEgresoConDetallesDTO
                {
                    Fecha = ordenIngreso.Fecha,
                    Destino = ordenIngreso.Destino,
                    Estado = ordenIngreso.Estado,
                    Detalles = detalles.Select(d => new OrdenEgresoDetallesDTO
                    {
                        IdProductoProveedor = d.IdProductoProveedor,
                        Cantidad = d.Cantidad,
                        TipoEgreso = d.TipoEgreso,
                        UbicacionId = d.UbicacionId,
                    }).ToList()
                };
                result.Add(ordenEgresoDTO);
            }
            return result;
        }
    }
}
