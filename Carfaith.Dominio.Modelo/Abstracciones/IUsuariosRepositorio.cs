using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IUsuariosRepositorio : IRepositorio<Usuarios>
    {
        public Task<bool> IsEmailUnique(string email);
    }
}
