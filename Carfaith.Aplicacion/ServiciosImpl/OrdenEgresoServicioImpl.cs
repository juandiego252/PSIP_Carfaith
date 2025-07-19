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
    public class OrdenEgresoServicioImpl : IOrdenEgresoServicio
    {
        private readonly IOrdenEgresoRepositorio _ordenEgresoRepositorio;

        public OrdenEgresoServicioImpl(CarfaithDbContext _context)
        {
            _ordenEgresoRepositorio = new OrdenEgresoRepositorioImpl(_context);
        }
        public async Task AddOrdenEgresoAsync(OrdenEgreso ordenEgreso)
        {
            await _ordenEgresoRepositorio.AddAsync(ordenEgreso);
        }

        public async Task DeleteOrdenEgresoAsync(int id)
        {
            await _ordenEgresoRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrdenEgreso>> GetAllOrdenEgresosAsync()
        {
            return await _ordenEgresoRepositorio.GetAllAsync();
        }

        public async Task<OrdenEgreso> GetOrdenEgresoByIdAsync(int id)
        {
            return await _ordenEgresoRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateOrdenEgresoAsync(OrdenEgreso ordenEgreso)
        {
            await _ordenEgresoRepositorio.UpdateAsync(ordenEgreso);
        }
    }
}
