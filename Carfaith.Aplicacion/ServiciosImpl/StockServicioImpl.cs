using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class StockServicioImpl : IStockServicio
    {
        private readonly IStockRepositorio _stockRepositorio;

        public StockServicioImpl(CarfaithDbContext context)
        {
            _stockRepositorio = new StockRepositorioImpl(context);
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _stockRepositorio.AddAsync(stock);
        }

        public async Task DeleteStockAsync(int id)
        {
            await _stockRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<Stock>> GetAllStockAsync()
        {
            return await _stockRepositorio.GetAllAsync();
        }

        public async Task<Stock> GetStockById(int id)
        {
            return await _stockRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateStockAsync(Stock stock)
        {
            await _stockRepositorio.UpdateAsync(stock);
        }
    }
}
