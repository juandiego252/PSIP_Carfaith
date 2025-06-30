using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class RepositorioImpl<T> : IRepositorio<T> where T : class
    {
        private readonly CarfaithDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositorioImpl(CarfaithDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entidad)
        {
            try
            {
                await _dbSet.AddAsync(entidad);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new Exception("Error no se pudo insertar datos: " + e.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entidad = await GetByIdAsync(id) ?? throw new Exception("Entidad no encontrada para eliminar");
                _dbSet.Remove(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar la entidad: " + e.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error al listar: " + e.Message);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var result = await _dbSet.FindAsync(id);
                if (result == null)
                {
                    throw new Exception("Entidad no encontrada");
                }
                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener la entidad: " + e.Message);
            }
        }

        public async Task UpdateAsync(T entidad)
        {
            try
            {
                _dbSet.Update(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar: " + e.Message);
            }
        }
    }
}
