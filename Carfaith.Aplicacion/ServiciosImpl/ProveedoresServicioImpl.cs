using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.Proveedor;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class ProveedoresServicioImpl : IProveedoresServicio
    {
        private IProveedoresRepositorio _proveedoresRepositorio;

        public ProveedoresServicioImpl(CarfaithDbContext context)
        {
            _proveedoresRepositorio = new ProveedoresRepositorioImpl(context);
        }

        // Metodos CRUD
        public async Task AddProveedoresAsync(Proveedores proveedores)
        {
            if (proveedores == null)
            {
                throw new ArgumentNullException("El proveedor no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(proveedores.Ruc))
            {
                throw new ArgumentException("El RUC del proveedor no puede ser nulo o vacío.");
            }

            var existeProveedor = await _proveedoresRepositorio.GetProveedoresPorRucAsync(proveedores.Ruc);
            if (existeProveedor.Any())
            {
                throw new ArgumentException("El proveedor ya se encuentra registrado");
            }

            ValidarTipoProveedor(proveedores.TipoProveedor!);
            await _proveedoresRepositorio.AddAsync(proveedores);
        }

        public async Task DeleteProveedoresByIdAsync(int id)
        {
            await _proveedoresRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<Proveedores>> GetAllProveedoresAsync()
        {
            return await _proveedoresRepositorio.GetAllAsync();
        }

        public async Task<Proveedores> GetByIdProveedoresAsync(int id)
        {
            return await _proveedoresRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateProveedoresAsync(Proveedores proveedores)
        {
            var existeProveedor = await _proveedoresRepositorio.GetByIdAsync(proveedores.IdProveedor);
            if (existeProveedor == null)
            {
                throw new ArgumentException("El proveedor no existe.");
            }
            // Mapear propiedades actualizadas
            existeProveedor.NombreProveedor = proveedores.NombreProveedor;
            existeProveedor.PaisOrigen = proveedores.PaisOrigen;
            existeProveedor.TipoProveedor = proveedores.TipoProveedor;
            existeProveedor.Telefono = proveedores.Telefono;
            existeProveedor.Email = proveedores.Email;
            existeProveedor.PersonaContacto = proveedores.PersonaContacto;
            existeProveedor.FechaRegistro = proveedores.FechaRegistro;
            existeProveedor.Ruc = proveedores.Ruc;
            existeProveedor.Direccion = proveedores.Direccion;
            existeProveedor.Estado = proveedores.Estado;
            await _proveedoresRepositorio.UpdateAsync(existeProveedor);
        }

        // Consultas
        public async Task<IEnumerable<Proveedores>> GetProveedoresPorNombreAsync(string nombre)
        {
            return await _proveedoresRepositorio.GetProveedoresPorNombreAsync(nombre);
        }

        public async Task<IEnumerable<Proveedores>> GetProveedoresPorPais(string paisOrigen)
        {
            return await _proveedoresRepositorio.GetProveedoresPorPais(paisOrigen);
        }

        public async Task<IEnumerable<Proveedores>> GetProveedoresPorTipoProveedor(string tipoProveedor)
        {
            return await _proveedoresRepositorio.GetProveedoresPorTipoProveedor(tipoProveedor);
        }


        // Metodos de Validación
        public void ValidarTipoProveedor(string tipoProveedor)
        {
            string[] tiposValidos = { "local", "nacional", "internacional" };
            if (!tiposValidos.Contains(tipoProveedor.ToLower()))
            {
                throw new ArgumentException("Proveedor no valido. Debe ser local, nacional o internacional.");
            }
        }

        public async Task<IEnumerable<ProveedorDetalleDTO>> GetProveedoresConDetallesAsync()
        {
            return await _proveedoresRepositorio.GetProveedoresConDetallesAsync();
        }
    }
}
