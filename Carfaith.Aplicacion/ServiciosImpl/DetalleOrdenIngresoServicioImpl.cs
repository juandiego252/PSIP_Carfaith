using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class DetalleOrdenIngresoServicioImpl : IDetalleOrdenIngresoServicio
    {
        private readonly IDetalleOrdenIngresoRepositorio _detalleOrdenIngresoRepositorio;

        public DetalleOrdenIngresoServicioImpl(CarfaithDbContext _context)
        {
            _detalleOrdenIngresoRepositorio = new DetalleOrdenIngresoRepositorioImpl(_context);
        }

        public async Task AddDetalleOrdenIngresoAsync(DetalleOrdenIngreso detalleOrdenIngreso)
        {
            await _detalleOrdenIngresoRepositorio.AddAsync(detalleOrdenIngreso);
        }

        public async Task DeleteDetalleOrdenIngresoByIdAsync(int id)
        {
            await _detalleOrdenIngresoRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<DetalleOrdenIngreso>> GetAllDetalleOrdenIngresoAsync()
        {
            return await _detalleOrdenIngresoRepositorio.GetAllAsync();
        }

        public async Task<DetalleOrdenIngreso> GetByIdDetalleOrdenIngresoAsync(int id)
        {
            return await _detalleOrdenIngresoRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateDetalleOrdenIngresoAsync(DetalleOrdenIngreso detalleOrdenIngreso)
        {
            await _detalleOrdenIngresoRepositorio.UpdateAsync(detalleOrdenIngreso);
        }
    }
}
