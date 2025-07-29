using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenCompra;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenIngreso;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class OrdenCompraConDetalleServicioImpl : IOrdenCompraConDetalleServicio
    {
        private readonly CarfaithDbContext _context;
        private readonly IOrdenDeCompraServicio _ordenCompraServicio;
        private readonly IDetalleOrdenCompraServicio _detalleOrdenCompraServicio;

        public OrdenCompraConDetalleServicioImpl(CarfaithDbContext context, IOrdenDeCompraServicio ordenCompraServicio, IDetalleOrdenCompraServicio detalleOrdenCompraServicio)
        {
            _context = context;
            _ordenCompraServicio = ordenCompraServicio;
            _detalleOrdenCompraServicio = detalleOrdenCompraServicio;
        }

        public async Task<int> CrearOrdenCompraConDetalles(OrdenCompraConDetallesDTO ordenCompraConDetallesDTO)
        {
            if (ordenCompraConDetallesDTO == null)
            {
                throw new ArgumentNullException(nameof(ordenCompraConDetallesDTO), "El DTO de orden de compra no puede ser nulo.");
            }

            if (ordenCompraConDetallesDTO.Detalles == null || !ordenCompraConDetallesDTO.Detalles.Any())
            {
                throw new ArgumentException("La orden de compra debe tener al menos un detalle");
            }

            foreach (var detalle in ordenCompraConDetallesDTO.Detalles)
            {
                if (!detalle.IdProductoProveedor.HasValue || !detalle.PrecioUnitario.HasValue || !detalle.Cantidad.HasValue)
                {
                    throw new ArgumentException("Todos los detalles deben especificar producto, cantidad y precio unitario.");
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var ordenCompra = new OrdenDeCompra
                    {
                        NumeroOrden = ordenCompraConDetallesDTO.NumeroOrden,
                        IdProveedor = ordenCompraConDetallesDTO.IdProveedor,
                        ArchivoPdf = ordenCompraConDetallesDTO.ArchivoPdf,
                        Estado = ordenCompraConDetallesDTO.Estado,
                        FechaCreacion = ordenCompraConDetallesDTO.FechaCreacion ?? DateOnly.FromDateTime(DateTime.Now),
                        FechaEstimadaEntrega = ordenCompraConDetallesDTO.FechaEstimadaEntrega ?? DateOnly.FromDateTime(DateTime.Now),
                    };

                    await _ordenCompraServicio.AddOrdenDeCompraAsync(ordenCompra);

                    // 2. Crear los detalles de la orden de compra y reducir el stock

                    foreach (var detalleDTO in ordenCompraConDetallesDTO.Detalles)
                    {
                        var detalle = new DetalleOrdenCompra
                        {
                            IdOrden = ordenCompra.IdOrden,
                            IdProductoProveedor = detalleDTO.IdProductoProveedor,
                            Cantidad = detalleDTO.Cantidad,
                            PrecioUnitario = detalleDTO.PrecioUnitario
                        };

                        await _detalleOrdenCompraServicio.AddDetalleOrdenCompra(detalle);
                    }

                    scope.Complete();
                    return ordenCompra.IdOrden;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<int> EditarOrdenCompraConDetalles(OrdenCompraConDetallesDTO ordenCompraConDetallesDTO)
        {
            if (ordenCompraConDetallesDTO == null)
            {
                throw new ArgumentNullException(nameof(ordenCompraConDetallesDTO), "El DTO de orden de compra no puede ser nulo.");
            }

            if (ordenCompraConDetallesDTO.Detalles == null || !ordenCompraConDetallesDTO.Detalles.Any())
            {
                throw new ArgumentException("La orden de compra debe tener al menos un detalle");
            }

            var ordenCompraExistente = await _ordenCompraServicio.GetByIdOrdenDeCompraAsync(ordenCompraConDetallesDTO.IdOrden.Value);
            if (ordenCompraExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró una orden de compra con el ID {ordenCompraConDetallesDTO.IdOrden}");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    ordenCompraExistente.NumeroOrden = ordenCompraConDetallesDTO.NumeroOrden;
                    ordenCompraExistente.IdProveedor = ordenCompraConDetallesDTO.IdProveedor;
                    ordenCompraExistente.ArchivoPdf = ordenCompraConDetallesDTO.ArchivoPdf;
                    ordenCompraExistente.Estado = ordenCompraConDetallesDTO.Estado;
                    ordenCompraExistente.FechaCreacion = ordenCompraConDetallesDTO.FechaCreacion;
                    ordenCompraExistente.FechaEstimadaEntrega = ordenCompraConDetallesDTO.FechaEstimadaEntrega;

                    await _ordenCompraServicio.UpdateOrdenDeCompraAsync(ordenCompraExistente);

                    var detallesActuales = await _context.DetalleOrdenCompras
                        .Where(d => d.IdOrden == ordenCompraExistente.IdOrden)
                        .ToListAsync();

                    foreach (var detalle in detallesActuales)
                    {
                        await _detalleOrdenCompraServicio.DeleteDetalleOrdenCompra(detalle.IdDetalleOrden);
                    }

                    foreach (var detalleDTO in ordenCompraConDetallesDTO.Detalles)
                    {
                        var nuevoDetalle = new DetalleOrdenCompra
                        {
                            IdOrden = ordenCompraExistente.IdOrden,
                            IdProductoProveedor = detalleDTO.IdProductoProveedor,
                            PrecioUnitario = detalleDTO.PrecioUnitario,
                            Cantidad = detalleDTO.Cantidad
                        };

                        await _detalleOrdenCompraServicio.AddDetalleOrdenCompra(nuevoDetalle);
                    }

                    scope.Complete();
                    return ordenCompraExistente.IdOrden;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task EliminarOrdenCompraConDetalles(int idOrdenCompra)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // 1. Obtener la orden y sus detalles
                    var orden = await _ordenCompraServicio.GetByIdOrdenDeCompraAsync(idOrdenCompra);
                    if (orden == null)
                    {
                        throw new KeyNotFoundException($"No se encontró la orden de compra con ID {idOrdenCompra}");
                    }

                    // 2. Obtener los detalles para actualizar el stock
                    var detalles = await _context.DetalleOrdenCompras
                        .Where(d => d.IdOrden == idOrdenCompra)
                        .ToListAsync();

                    // 4. Eliminar cada detalle manualmente
                    foreach (var detalle in detalles)
                    {
                        await _detalleOrdenCompraServicio.DeleteDetalleOrdenCompra(detalle.IdDetalleOrden);
                    }

                    // 5. Eliminar la orden de egreso
                    await _ordenCompraServicio.DeleteOrdenDeCompraByIdAsync(idOrdenCompra);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, la transacción se revierte automáticamente
                    throw new Exception($"Error al eliminar la orden de compra: {ex.Message}", ex);
                }
            }
        }

        public async Task<IEnumerable<OrdenCompraConDetallesDTO>> GetOrdenCompraConDetalles()
        {
            var ordenesCompra = await _ordenCompraServicio.GetAllOrdenDeCompraAsync();
            var result = new List<OrdenCompraConDetallesDTO>();

            foreach (var ordenCompra in ordenesCompra)
            {
                var detalles = await _context.DetalleOrdenCompras.Where(d => d.IdOrden == ordenCompra.idOrden).ToListAsync();

                var proveedor = await _context.Proveedores
                    .FirstOrDefaultAsync(p => p.IdProveedor == ordenCompra.idProveedor);

                var ordenEgresoDTO = new OrdenCompraConDetallesDTO
                {
                    IdOrden = ordenCompra.idOrden,
                    NumeroOrden = ordenCompra.numeroOrden,
                    IdProveedor = ordenCompra.idProveedor,
                    NombreProveedor = proveedor?.NombreProveedor,
                    ArchivoPdf = ordenCompra.archivoPdf,
                    Estado = ordenCompra.estado,
                    FechaCreacion = ordenCompra.fechaCreacion,
                    FechaEstimadaEntrega = ordenCompra.fechaEstimadaEntrega,
                    Detalles = detalles.Select(d => new OrdenCompraDetallesDTO
                    {
                        IdProductoProveedor = d.IdProductoProveedor,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList()
                };
                result.Add(ordenEgresoDTO);
            }
            return result;
        }
    }
}
