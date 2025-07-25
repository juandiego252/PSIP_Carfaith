using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;

namespace Carfaith.Aplicacion.ServiciosImpl
{

    public class OrdenDeIngresoServicioImpl : IOrdenDeIngresoServicio
    {
        private readonly IOrdenDeIngresoRepositorio _ordenDeIngresoRepositorio;

        public OrdenDeIngresoServicioImpl(CarfaithDbContext context)
        {
            _ordenDeIngresoRepositorio = new OrdenDeIngresoRepositorioImpl(context);
        }
        public async Task AddOrdenDeIngresoAsync(OrdenDeIngreso ordenDeIngreso)
        {
            await _ordenDeIngresoRepositorio.AddAsync(ordenDeIngreso);
        }

        public async Task DeleteOrdenDeIngresoByIdAsync(int id)
        {
            await _ordenDeIngresoRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrdenDeIngreso>> GetAllOrdenDeIngresoAsync()
        {
            return await _ordenDeIngresoRepositorio.GetAllAsync();
        }

        public Task<OrdenDeIngreso> GetByIdOrdenDeIngresoAsync(int id)
        {
            return _ordenDeIngresoRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateOrdenDeIngresoAsync(OrdenDeIngreso ordenDeIngreso)
        {
            var existeOrdenIngreso = await _ordenDeIngresoRepositorio.GetByIdAsync(ordenDeIngreso.IdOrdenIngreso);

            if (existeOrdenIngreso != null)
            {
                existeOrdenIngreso.OrigenDeCompra = ordenDeIngreso.OrigenDeCompra;
                existeOrdenIngreso.IdOrdenCompra = ordenDeIngreso.IdOrdenCompra;
                existeOrdenIngreso.Fecha = ordenDeIngreso.Fecha;
                existeOrdenIngreso.Estado = ordenDeIngreso.Estado;
            }
            else
            {
                throw new KeyNotFoundException($"Orden de ingreso con ID {ordenDeIngreso.IdOrdenIngreso} no encontrada.");
            }

            await _ordenDeIngresoRepositorio.UpdateAsync(existeOrdenIngreso!);

        }

        // Consulta 
        public async Task<IEnumerable<OrdenDeIngresoOrdenCompraDTO>> OrdenDeIngresoOrdenCompraDTOs()
        {
            return await _ordenDeIngresoRepositorio.GetOrdenDeIngresoOrdenCompraDTOs();
        }
    }
}
