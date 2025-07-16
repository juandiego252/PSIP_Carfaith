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
    public class UbicacionesServicioImpl : IUbicacionesServicio
    {
        private readonly IUbicacionesRepositorio _ubicacionesRepositorio;

        public UbicacionesServicioImpl(CarfaithDbContext context)
        {
            _ubicacionesRepositorio = new UbicacionesRepositorioImpl(context);
        }
        public async Task AddUbicacionesAsync(Ubicaciones ubicaciones)
        {
            await _ubicacionesRepositorio.AddAsync(ubicaciones);
        }

        public async Task DeleteUbicacionesAsync(int id)
        {
            await _ubicacionesRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<Ubicaciones>> GetAllUbicacionesAsync()
        {
            return await _ubicacionesRepositorio.GetAllAsync();
        }

        public async Task<Ubicaciones> GetUbicacionesById(int id)
        {
            return await _ubicacionesRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateUbicacionesAsync(Ubicaciones ubicaciones)
        {
            await _ubicacionesRepositorio.UpdateAsync(ubicaciones);
        }
    }
}
