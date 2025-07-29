using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class DetalleOrdenCompraServicioImpl : IDetalleOrdenCompraServicio
    {
        private readonly IDetalleOrdenCompraRepositorio _detalleOrdenCompraRepositorio;

        public DetalleOrdenCompraServicioImpl(CarfaithDbContext _context)
        {
            _detalleOrdenCompraRepositorio = new DetalleOrdenCompraRepositorioImpl(_context);
        }

        public async Task AddDetalleOrdenCompra(DetalleOrdenCompra detalleOrdenCompra)
        {
            await _detalleOrdenCompraRepositorio.AddAsync(detalleOrdenCompra);
        }

        public async Task DeleteDetalleOrdenCompra(int id)
        {
            await _detalleOrdenCompraRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<DetalleOrdenCompra>> GetAllDetallesOrdenCompraAsync()
        {
            return await _detalleOrdenCompraRepositorio.GetAllAsync();
        }

        public async Task<DetalleOrdenCompra> GetDetalleOrdenCompraById(int id)
        {
            return await _detalleOrdenCompraRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateDetalleOrdenCompra(DetalleOrdenCompra detalleOrdenCompra)
        {
            await _detalleOrdenCompraRepositorio.UpdateAsync(detalleOrdenCompra);
        }
    }
}
