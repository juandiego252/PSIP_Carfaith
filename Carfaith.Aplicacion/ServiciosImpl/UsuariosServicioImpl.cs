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

            if (string.IsNullOrEmpty(usuario.NombreCompleto))
            {
                throw new ArgumentException("El nombre completo no puede ser nulo o vacío.", nameof(usuario));
            }
            // Verificar que el email sea unico
            if (!string.IsNullOrEmpty(usuario.Email) && !await _usuariosRepositorio.IsEmailUnique(usuario.Email))
            {
                throw new ArgumentException($"El email {usuario.Email} ya se encuentra registrado", nameof(usuario));
            }

            if (string.IsNullOrEmpty(usuario.Contraseña))
            {
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(usuario));
            }

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            await _usuariosRepositorio.AddAsync(usuario);
        }

        public async Task DeleteUsuariosByIdAsync(int id)
        {
            await _usuariosRepositorio.DeleteAsync(id);
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
            var usuarioExistente = await _usuariosRepositorio.GetByIdAsync(usuario.IdUsuario);
            if (usuarioExistente == null)
            {
                throw new ArgumentException($"No se encontró un usuario con el ID: {usuario.IdUsuario}");
            }

            if (!string.IsNullOrEmpty(usuario.NombreCompleto))
            {
                usuarioExistente.NombreCompleto = usuario.NombreCompleto;
            }

            if (!string.IsNullOrEmpty(usuario.Email))
            {
                if (usuario.Email != usuarioExistente.Email && !await _usuariosRepositorio.IsEmailUnique(usuario.Email))
                {
                    throw new ArgumentException($"El email {usuario.Email} ya se encuentra registrado");
                }
                usuarioExistente.Email = usuario.Email;
            }

            if (!string.IsNullOrEmpty(usuario.Contraseña))
            {
                usuarioExistente.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
            }

            await _usuariosRepositorio.UpdateAsync(usuarioExistente);
        }
    }
}
