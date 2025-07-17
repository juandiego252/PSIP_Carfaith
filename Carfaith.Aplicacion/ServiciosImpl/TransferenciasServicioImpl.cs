using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class TransferenciasServicioImpl : ITransferenciasServicio
    {
        private readonly ITransferenciasRepositorio _transferenciasRepositorio;
        private readonly CarfaithDbContext _dbContext;

        public TransferenciasServicioImpl(CarfaithDbContext context)
        {
            _dbContext = context;
            this._transferenciasRepositorio = new TransferenciasRepositorioImpl(_dbContext);
        }

        public async Task AddTransferenciasAsync(Transferencias transferencias)
        {
            await _transferenciasRepositorio.AddAsync(transferencias);
        }

        public async Task DeleteTransferenciasAsync(int id)
        {
            await _transferenciasRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<Transferencias>> GetAllTransferenciasAsync()
        {
            return await _transferenciasRepositorio.GetAllAsync();
        }

        public async Task<Transferencias> GetTransferenciasById(int id)
        {
            return await _transferenciasRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateTransferenciasAsync(Transferencias transferencias)
        {
            await _transferenciasRepositorio.UpdateAsync(transferencias);
        }
    }
}
