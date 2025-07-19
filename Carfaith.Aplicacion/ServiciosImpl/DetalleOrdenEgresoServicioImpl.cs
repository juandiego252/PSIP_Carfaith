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

        public Task DeleteDetalleOrdenEgreso(int detalleOrdenEgresoId)
        {
            throw new NotImplementedException();
        }

        public Task<DetalleOrdenEgreso> GetDetalleOrdenEgresoById(int detalleOrdenEgresoId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DetalleOrdenEgreso>> GetDetallesOrdenEgresoByOrdenId(int ordenEgresoId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDetalleOrdenEgreso(DetalleOrdenEgreso detalleOrdenEgreso)
        {
            throw new NotImplementedException();
        }
    }
}
