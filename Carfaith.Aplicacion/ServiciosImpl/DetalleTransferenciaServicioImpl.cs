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
    public class DetalleTransferenciaServicioImpl : IDetalleTransferenciaServicio
    {
        private readonly IDetalleTransferenciaRepositorio _detalleTransferenciaRepositorio;

        public DetalleTransferenciaServicioImpl(CarfaithDbContext context)
        {
            _detalleTransferenciaRepositorio = new DetalleTransferenciaRepositorioImpl(context);
        }
        public async Task AddDetalleTransferenciaAsync(DetalleTransferencia detalleTransferencia)
        {
            await _detalleTransferenciaRepositorio.AddAsync(detalleTransferencia);
        }

        public async Task DeleteDetalleTransferenciaAsync(int id)
        {
            await _detalleTransferenciaRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<DetalleTransferencia>> GetAllDetalleTransferenciaAsync()
        {
            return await _detalleTransferenciaRepositorio.GetAllAsync();
        }

        public async Task<IEnumerable<DetalleTransferencia>> GetDetalleTransferenciaByIdAsync(int id)
        {
            return await _detalleTransferenciaRepositorio.GetDetallesByTransferenciaIdAsync(id);
        }

        public async Task UpdateDetalleTransferenciaAsync(DetalleTransferencia detalleTransferencia)
        {
            await _detalleTransferenciaRepositorio.UpdateAsync(detalleTransferencia);
        }

    }
}
