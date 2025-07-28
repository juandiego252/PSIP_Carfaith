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
    public class DetalleOrdenEgresoServicioImpl : IDetalleOrdenEgresoServicio
    {
        private readonly IDetalleOrdenEgresoRepositorio _detalleOrdenEgresoRepositorio;

        public DetalleOrdenEgresoServicioImpl(CarfaithDbContext _context)
        {
            _detalleOrdenEgresoRepositorio = new DetalleOrdenEgresoRepositorioImpl(_context);
        }

        public async Task AddDetalleOrdenEgreso(DetalleOrdenEgreso detalleOrdenEgreso)
        {
            await _detalleOrdenEgresoRepositorio.AddAsync(detalleOrdenEgreso);
        }

        public async Task DeleteDetalleOrdenEgreso(int detalleOrdenEgresoId)
        {
            await _detalleOrdenEgresoRepositorio.DeleteAsync(detalleOrdenEgresoId);
        }

        public async Task<IEnumerable<DetalleOrdenEgreso>> GetAllDetallesOrdenEgresoAsync()
        {
            return await _detalleOrdenEgresoRepositorio.GetAllAsync();
        }

        public async Task<DetalleOrdenEgreso> GetDetalleOrdenEgresoById(int detalleOrdenEgresoId)
        {
            return await _detalleOrdenEgresoRepositorio.GetByIdAsync(detalleOrdenEgresoId);
        }

        /* public async Task<List<DetalleOrdenEgreso>> GetDetallesOrdenEgresoByOrdenId(int ordenEgresoId)
        {
            return await _detalleOrdenEgresoRepositorio.GetByIdAsync(ordenEgresoId);
        } */

        public async Task UpdateDetalleOrdenEgreso(DetalleOrdenEgreso detalleOrdenEgreso)
        {
            await _detalleOrdenEgresoRepositorio.UpdateAsync(detalleOrdenEgreso);
        }
    }
}
