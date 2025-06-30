using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IRepositorio<T> where T : class
    {
        Task AddAsync(T entidad);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entidad);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
