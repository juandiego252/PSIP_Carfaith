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
    public class UsuariosServicioImpl : IUsuariosServicio
    {
        private IUsuariosRepositorio _usuariosRepositorio;

        public UsuariosServicioImpl(CarfaithDbContext context)
        {
            _usuariosRepositorio = new UsuariosRepositorioImpl(context);
        }
        public async Task AddUsuariosAsync(Usuarios usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
            }

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            await _usuariosRepositorio.AddAsync(usuario);
        }

        public async Task DeleteUsuariosAsync(int id)
        {
            var usuario = await _usuariosRepositorio.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
            }
            await _usuariosRepositorio.DeleteAsync(usuario.IdUsuario);
        }

        public async Task<IEnumerable<Usuarios>> GetAllUsuariosAsync()
        {
            return await _usuariosRepositorio.GetAllAsync();
        }

        public async Task<Usuarios> GetByIdUsuariosAsync(int id)
        {
            return await _usuariosRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateUsuariosAsync(Usuarios usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
            }

            if (!string.IsNullOrEmpty(usuario.Contraseña))
            {
                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
            }
            else
            {
                var usuarioExistente = await _usuariosRepositorio.GetByIdAsync(usuario.IdUsuario);
                if (usuarioExistente != null)
                {
                    usuario.Contraseña = usuarioExistente.Contraseña; // Mantener la contraseña existente si no se proporciona una nueva
                }
            }

            await _usuariosRepositorio.UpdateAsync(usuario);
        }
    }
}
