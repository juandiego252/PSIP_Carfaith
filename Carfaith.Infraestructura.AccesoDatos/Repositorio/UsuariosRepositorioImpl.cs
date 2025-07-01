using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class UsuariosRepositorioImpl : RepositorioImpl<Usuarios>, IUsuariosRepositorio
    {
        private readonly CarfaithDbContext _context;
        public UsuariosRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            try
            {
                var emailExists = from u in _context.Usuarios
                                  where u.Email == email
                                  select u;

                return await emailExists.AnyAsync() == false;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la unicidad del email: " + ex.Message);
            }
        }
    }
}
