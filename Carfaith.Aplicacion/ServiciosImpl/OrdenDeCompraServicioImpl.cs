using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class OrdenDeCompraServicioImpl : IOrdenDeCompraServicio
    {
        private readonly IOrdenDeCompraRepositorio _ordenDeCompraRepositorio;

        public OrdenDeCompraServicioImpl(CarfaithDbContext context)
        {
            _ordenDeCompraRepositorio = new OrdenDeCompraRepositorioImpl(context);
        }
        public async Task AddOrdenDeCompraAsync(OrdenDeCompra ordenDeCompra)
        {
            await _ordenDeCompraRepositorio.AddAsync(ordenDeCompra);
        }

        public async Task DeleteOrdenDeCompraByIdAsync(int id)
        {
            await _ordenDeCompraRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrdenDeCompra>> GetAllOrdenDeCompraAsync()
        {
            return await _ordenDeCompraRepositorio.GetAllAsync();
        }

        public async Task<OrdenDeCompra> GetByIdOrdenDeCompraAsync(int id)
        {
            return await _ordenDeCompraRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateOrdenDeCompraAsync(OrdenDeCompra ordenDeCompra)
        {
            var ordenCompraExistente = await _ordenDeCompraRepositorio.GetByIdAsync(ordenDeCompra.IdOrden);
            if (ordenCompraExistente == null)
            {
                throw new ArgumentException($"No se encontró una orden de compra con ID {ordenDeCompra.IdOrden}", nameof(ordenDeCompra));
            }

            //Actualizar los campos necesarios
            ordenCompraExistente.NumeroOrden = ordenDeCompra.NumeroOrden;
            ordenCompraExistente.FechaCreacion = ordenDeCompra.FechaCreacion;
            ordenCompraExistente.IdProveedor = ordenDeCompra.IdProveedor;
            ordenCompraExistente.Estado = ordenDeCompra.Estado;
            ordenCompraExistente.FechaEstimadaEntrega = ordenDeCompra.FechaEstimadaEntrega;
            ordenCompraExistente.ArchivoPdf = ordenDeCompra.ArchivoPdf;

            await _ordenDeCompraRepositorio.UpdateAsync(ordenCompraExistente);
        }
    }
}
