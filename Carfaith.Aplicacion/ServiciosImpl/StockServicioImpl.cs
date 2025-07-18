using Carfaith.Aplicacion.DTO.DTOs;
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
            if (stock.Cantidad < 0)
            {
                throw new ArgumentException("La cantidad del stock no puede ser negativa.");
            }

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

        public async Task<IEnumerable<StockProductoProveedorUbicacionDTO>> GetstockProductoProveedorUbicacionDTOs()
        {
            return await _stockRepositorio.GetStockProductoProveedorUbicacionDto();
        }

        public async Task<Stock> GetStockByProductoProveedorYUbicacionAsync(int idProductoProveedor, int idUbicacion)
        {
            return await _stockRepositorio.GetStockByProductoProveedorYUbicacionAsync(idProductoProveedor, idUbicacion);
        }

        public async Task<Stock> ActualizarStockPorTransferenciaAsync(int idProductoProveedor, int idUbicacion, int cantidad)
        {
            return await _stockRepositorio.ActualizarStockPorTransferenciaAsync(idProductoProveedor, idUbicacion, cantidad);
        }
    }
}
