using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Carfaith.Aplicacion.DTO.DTOs.Transferencias;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class TransferenciaStockServicioImpl : ITransferenciaStockServicio
    {
        private readonly IDetalleTransferenciaServicio _detalleTransferenciaServicio;
        private readonly IStockServicio _stockServicio;
        private readonly IUbicacionesServicio _ubicacionesServicio;
        private readonly IProductoProveedorServicio _productoProveedorServicio;
        private readonly ITransferenciasServicio _transferenciasServicio;

        public TransferenciaStockServicioImpl(
            IDetalleTransferenciaServicio detalleTransferenciaServicio,
            IStockServicio stockServicio,
            IUbicacionesServicio ubicacionesServicio,
            IProductoProveedorServicio productoProveedorServicio,
            ITransferenciasServicio transferenciasServicio
            )
        {
            _detalleTransferenciaServicio = detalleTransferenciaServicio;
            _stockServicio = stockServicio;
            _ubicacionesServicio = ubicacionesServicio;
            _productoProveedorServicio = productoProveedorServicio;
            _transferenciasServicio = transferenciasServicio;
        }
        public async Task<IEnumerable<TransferenciaConDetallesDTO>> GetTransferenciasConDetalles()
        {
            var transferencias = await _transferenciasServicio.GetAllTransferenciasWithRelationsAsync();
            var result = new List<TransferenciaConDetallesDTO>();

            foreach (var transferencia in transferencias)
            {
                var detalles = await _detalleTransferenciaServicio.GetDetalleTransferenciaByIdAsync(transferencia.IdTransferencia);

                var transferenciasDTO = new TransferenciaConDetallesDTO
                {
                    IdTransferencia = transferencia.IdTransferencia,
                    Fecha = transferencia.Fecha,
                    UbicacionOrigenId = transferencia.UbicacionOrigenId,
                    UbicacionDestinoId = transferencia.UbicacionDestinoId,
                    UbicacionOrigenNombre = transferencia.UbicacionOrigen?.LugarUbicacion,
                    UbicacionDestinoNombre = transferencia.UbicacionDestino?.LugarUbicacion,
                    Detalles = detalles.Select(d => new DetalleTransferenciaInfoDTO
                    {
                        IdDetalleTransferencia = d.IdDetalleTransferencia,
                        IdProductoProveedor = d.IdProductoProveedor,
                        NombreProducto = d.IdProductoProveedorNavigation?.IdProductoNavigation?.Nombre,
                        Cantidad = d.Cantidad
                    }).ToList()
                };

                result.Add(transferenciasDTO);
            }

            return result;
        }

        public async Task<int> ProcesarTransferenciaCompleta(TransferenciaStockDTO transferenciaDTO)
        {
            if (transferenciaDTO == null)
            {
                throw new ArgumentNullException(nameof(transferenciaDTO), "Los datos de la transferencia no pueden ser nulos.");
            }
            if (transferenciaDTO.Detalles == null || !transferenciaDTO.Detalles.Any())
            {
                throw new ArgumentException("La transferencia debe contener al menos un detalle.", nameof(transferenciaDTO.Detalles));
            }

            if (transferenciaDTO.UbicacionOrigenId == null || transferenciaDTO.UbicacionDestinoId == null)
            {
                throw new ArgumentException("Las ubicaciones de origen y destino deben estar definidas.", nameof(transferenciaDTO));
            }

            // Verificar que las ubicaciones existen

            var ubicacionOrigen = await _ubicacionesServicio.GetUbicacionesById(transferenciaDTO.UbicacionOrigenId.Value);
            var ubicacionDestino = await _ubicacionesServicio.GetUbicacionesById(transferenciaDTO.UbicacionDestinoId.Value);

            if (ubicacionOrigen == null)
            {
                throw new ArgumentException("La ubicación de origen no existe.", nameof(transferenciaDTO.UbicacionOrigenId));
            }
            if (ubicacionDestino == null)
            {
                throw new ArgumentException("La ubicación de destino no existe.", nameof(transferenciaDTO.UbicacionDestinoId));
            }

            // verificar stock suficiente para cada producto

            foreach (var detalle in transferenciaDTO.Detalles)
            {
                // verificar que el producto existe
                var productoExiste = await _productoProveedorServicio.GetByIdProductoProveedorAsync(detalle.IdProductoProveedor.Value);
                if (productoExiste == null)
                {
                    throw new ArgumentException($"El producto con ID {detalle.IdProductoProveedor} no existe", nameof(transferenciaDTO));
                }

                // verificar que el stock es suficiente

                var stock = await _stockServicio.GetStockByProductoProveedorYUbicacionAsync(detalle.IdProductoProveedor.Value, transferenciaDTO.UbicacionOrigenId.Value);

                if (stock == null || stock.Cantidad < detalle.Cantidad)
                {
                    throw new InvalidOperationException(
                        $"Stock insuficiente para el producto {detalle.IdProductoProveedor} en la ubicación de origen." +
                        $"Disponible: {(stock?.Cantidad ?? 0)}, Solicitado: {detalle.Cantidad}");
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // 1. Crear la transferencia
                    var transferencia = new Transferencias
                    {
                        Fecha = transferenciaDTO.Fecha ?? DateOnly.FromDateTime(DateTime.Now),
                        UbicacionOrigenId = transferenciaDTO.UbicacionOrigenId.Value,
                        UbicacionDestinoId = transferenciaDTO.UbicacionDestinoId.Value
                    };

                    await _transferenciasServicio.AddTransferenciasAsync(transferencia);

                    // 2. Crear los detalles de la transferencia y actualizar el stock

                    foreach (var detalleDTO in transferenciaDTO.Detalles)
                    {
                        var detalle = new DetalleTransferencia
                        {
                            IdTransferencia = transferencia.IdTransferencia, // Asignar el ID de la transferencia creada
                            IdProductoProveedor = detalleDTO.IdProductoProveedor!.Value,
                            Cantidad = detalleDTO.Cantidad
                        };
                        await _detalleTransferenciaServicio.AddDetalleTransferenciaAsync(detalle);

                        // 3. Reducir stock en origen

                        await _stockServicio.ActualizarStockPorTransferenciaAsync(
                            detalleDTO.IdProductoProveedor.Value,
                            transferenciaDTO.UbicacionOrigenId.Value,
                            -detalleDTO.Cantidad.Value);

                        // 4. Aumentar stock en destino
                        await _stockServicio.ActualizarStockPorTransferenciaAsync(
                            detalleDTO.IdProductoProveedor.Value,
                            transferenciaDTO.UbicacionDestinoId.Value,
                            detalleDTO.Cantidad.Value);
                    }
                    scope.Complete();
                    return transferencia.IdTransferencia;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<TransferenciaConDetallesDTO> GetTransferenciaConDetallesById(int idTransferencia)
        {
            var transferencia = await _transferenciasServicio.GetTransferenciaByIdWithRelationsAsync(idTransferencia);
            if (transferencia == null)
            {
                throw new KeyNotFoundException($"No se encontró la transferencia con ID {idTransferencia}");
            }

            var detalles = await _detalleTransferenciaServicio.GetDetalleTransferenciaByIdAsync(idTransferencia);

            return new TransferenciaConDetallesDTO
            {
                IdTransferencia = transferencia.IdTransferencia,
                Fecha = transferencia.Fecha,
                UbicacionOrigenId = transferencia.UbicacionOrigenId,
                UbicacionDestinoId = transferencia.UbicacionDestinoId,
                UbicacionOrigenNombre = transferencia.UbicacionOrigen?.LugarUbicacion,
                UbicacionDestinoNombre = transferencia.UbicacionDestino?.LugarUbicacion,
                Detalles = detalles.Select(d => new DetalleTransferenciaInfoDTO
                {
                    IdDetalleTransferencia = d.IdDetalleTransferencia,
                    IdProductoProveedor = d.IdProductoProveedor,
                    NombreProducto = d.IdProductoProveedorNavigation?.IdProductoNavigation?.Nombre,
                    Cantidad = d.Cantidad
                }).ToList()
            };
        }

    }

}
