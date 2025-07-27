using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Dominio.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IStockServicio
    {
        [OperationContract]
        Task AddStockAsync(Stock stock);

        [OperationContract]
        Task UpdateStockAsync(Stock stock);

        [OperationContract]
        Task DeleteStockAsync(int id);

        [OperationContract]
        Task<IEnumerable<Stock>> GetAllStockAsync();

        [OperationContract]
        Task<Stock> GetStockById(int id);

        // Consulta 
        [OperationContract]
        Task<IEnumerable<StockProductoProveedorUbicacionDTO>> GetstockProductoProveedorUbicacionDTOs();

        [OperationContract]
        Task<Stock> GetStockByProductoProveedorYUbicacionAsync(int idProductoProveedor, int idUbicacion);

        [OperationContract]
        Task<Stock> ActualizarStockPorTransferenciaAsync(int idProductoProveedor, int idUbicacion, int cantidad);


        [OperationContract]
        Task<Stock> ActualizarStockAsync(int idProductoProveedor, int idUbicacion, int cantidad);

    }
}
