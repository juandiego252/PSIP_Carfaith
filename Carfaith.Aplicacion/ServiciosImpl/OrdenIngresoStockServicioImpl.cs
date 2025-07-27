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
using Microsoft.EntityFrameworkCore;

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
                    var ordenIngreso = new OrdenDeIngreso
                    {
                        IdOrdenCompra = ordenIngresoConDetallesDTO.IdOrdenCompra,
                        OrigenDeCompra = ordenIngresoConDetallesDTO.OrigenDeCompra,
                        Fecha = ordenIngresoConDetallesDTO.Fecha,
                        Estado = ordenIngresoConDetallesDTO.Estado
                    };

                    await _ordenDeIngresoServicio.AddOrdenDeIngresoAsync(ordenIngreso);

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

                        await _stockServicio.ActualizarStockAsync(detalle.IdProductoProveedor.Value, detalle.UbicacionId.Value, detalle.Cantidad.Value);
                    }

                    scope.Complete();
                    return ordenIngreso.IdOrdenIngreso;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<int> EditarOrdenIngresoConDetalles(OrdenIngresoConDetallesDTO ordenIngresoConDetallesDTO)
        {
            if (ordenIngresoConDetallesDTO == null)
            {
                throw new ArgumentNullException(nameof(ordenIngresoConDetallesDTO), "El DTO de orden de ingreso no puede ser nulo.");
            }

            if (ordenIngresoConDetallesDTO.Detalles == null || !ordenIngresoConDetallesDTO.Detalles.Any())
            {
                throw new ArgumentException("La orden de ingreso debe tener al menos un detalle");
            }

            var ordenIngresoExistente = await _ordenDeIngresoServicio.GetByIdOrdenDeIngresoAsync(ordenIngresoConDetallesDTO.idOrdenIngreso.Value);
            if (ordenIngresoExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró una orden de ingreso con el ID {ordenIngresoConDetallesDTO.idOrdenIngreso}");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    ordenIngresoExistente.IdOrdenCompra = ordenIngresoConDetallesDTO.IdOrdenCompra;
                    ordenIngresoExistente.OrigenDeCompra = ordenIngresoConDetallesDTO.OrigenDeCompra;
                    ordenIngresoExistente.Fecha = ordenIngresoConDetallesDTO.Fecha;
                    ordenIngresoExistente.Estado = ordenIngresoConDetallesDTO.Estado;

                    await _ordenDeIngresoServicio.UpdateOrdenDeIngresoAsync(ordenIngresoExistente);

                    var detallesActuales = await _context.DetalleOrdenIngresos
                        .Where(d => d.OrdenIngresoId == ordenIngresoExistente.IdOrdenIngreso)
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
                        await _detalleOrdenIngresoServicio.DeleteDetalleOrdenIngresoByIdAsync(detalle.IdDetalleOrdenIngreso);
                    }

                    foreach (var detalleDTO in ordenIngresoConDetallesDTO.Detalles)
                    {
                        var nuevoDetalle = new DetalleOrdenIngreso
                        {
                            OrdenIngresoId = ordenIngresoExistente.IdOrdenIngreso,
                            IdProductoProveedor = detalleDTO.IdProductoProveedor,
                            Cantidad = detalleDTO.Cantidad,
                            PrecioUnitario = detalleDTO.PrecioUnitario,
                            UbicacionId = detalleDTO.UbicacionId,
                            TipoIngreso = detalleDTO.TipoIngreso,
                            NumeroLote = detalleDTO.NumeroLote
                        };

                        await _detalleOrdenIngresoServicio.AddDetalleOrdenIngresoAsync(nuevoDetalle);

                        if (nuevoDetalle.IdProductoProveedor.HasValue && nuevoDetalle.UbicacionId.HasValue && nuevoDetalle.Cantidad.HasValue)
                        {
                            await _stockServicio.ActualizarStockAsync(
                                nuevoDetalle.IdProductoProveedor.Value,
                                nuevoDetalle.UbicacionId.Value,
                                nuevoDetalle.Cantidad.Value);
                        }
                    }

                    scope.Complete();
                    return ordenIngresoExistente.IdOrdenIngreso;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task EliminarOrdenIngresoConDetalles(int idOrdenIngreso)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // 1. Obtener la orden y sus detalles
                    var orden = await _ordenDeIngresoServicio.GetByIdOrdenDeIngresoAsync(idOrdenIngreso);
                    if (orden == null)
                    {
                        throw new KeyNotFoundException($"No se encontró la orden de ingreso con ID {idOrdenIngreso}");
                    }

                    // 2. Obtener los detalles para actualizar el stock
                    var detalles = await _context.DetalleOrdenIngresos
                        .Where(d => d.OrdenIngresoId == idOrdenIngreso)
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
                        await _detalleOrdenIngresoServicio.DeleteDetalleOrdenIngresoByIdAsync(detalle.IdDetalleOrdenIngreso);
                    }

                    // 5. Eliminar la orden de ingreso
                    await _ordenDeIngresoServicio.DeleteOrdenDeIngresoByIdAsync(idOrdenIngreso);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, la transacción se revierte automáticamente
                    throw new Exception($"Error al eliminar la orden de ingreso: {ex.Message}", ex);
                }
            }
        }

        public async Task<IEnumerable<OrdenIngresoConDetallesDTO>> GetOrdenIngresoConDetalles()
        {
            var ordenesIngreso = await _ordenDeIngresoServicio.GetAllOrdenDeIngresoAsync();
            var result = new List<OrdenIngresoConDetallesDTO>();

            foreach (var ordenIngreso in ordenesIngreso)
            {
                var detalles = await _context.DetalleOrdenIngresos.Where(d => d.OrdenIngresoId == ordenIngreso.IdOrdenIngreso).ToListAsync();

                var ordenIngresoDTO = new OrdenIngresoConDetallesDTO
                {
                    idOrdenIngreso = ordenIngreso.IdOrdenIngreso,
                    IdOrdenCompra = ordenIngreso.IdOrdenCompra,
                    OrigenDeCompra = ordenIngreso.OrigenDeCompra,
                    Fecha = ordenIngreso.Fecha,
                    Estado = ordenIngreso.Estado,
                    Detalles = detalles.Select(d => new OrdenIngresoDetallesDTO
                    {
                        IdProductoProveedor = d.IdProductoProveedor,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        UbicacionId = d.UbicacionId,
                        TipoIngreso = d.TipoIngreso,
                        NumeroLote = d.NumeroLote
                    }).ToList()
                };
                result.Add(ordenIngresoDTO);
            }
            return result;
        }

    }
}
