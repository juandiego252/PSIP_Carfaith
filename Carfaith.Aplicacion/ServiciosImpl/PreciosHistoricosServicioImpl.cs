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
    public class PreciosHistoricosServicioImpl : IPreciosHistoricosServicio
    {
        private readonly IPreciosHistoricosRepositorio _preciosHistoricosRepositorio;

        public PreciosHistoricosServicioImpl(CarfaithDbContext _context)
        {
            _preciosHistoricosRepositorio = new PreciosHistoricosRepositorioImpl(_context);
        }

        public async Task AddPreciosHistoricosAsync(PreciosHistoricos preciosHistoricos)
        {
            await _preciosHistoricosRepositorio.AddAsync(preciosHistoricos);
        }

        public async Task DeletePreciosHistoricosByIdAsync(int id)
        {
            await _preciosHistoricosRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<PreciosHistoricos>> GetAllPreciosHistoricosAsync()
        {
            return await _preciosHistoricosRepositorio.GetAllAsync();
        }

        public async Task<PreciosHistoricos> GetByIdPreciosHistoricosAsync(int id)
        {
            return await _preciosHistoricosRepositorio.GetByIdAsync(id);
        }

        public async Task UpdatePreciosHistoricosAsync(PreciosHistoricos preciosHistoricos)
        {
            await _preciosHistoricosRepositorio.UpdateAsync(preciosHistoricos);   
        }
    }
}
