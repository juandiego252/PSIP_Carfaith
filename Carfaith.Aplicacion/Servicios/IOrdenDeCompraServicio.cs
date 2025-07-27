using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.OrdenDeCompra;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IOrdenDeCompraServicio
    {
        [OperationContract]
        Task AddOrdenDeCompraAsync(OrdenDeCompra ordenDeCompra);
        [OperationContract]
        Task UpdateOrdenDeCompraAsync(OrdenDeCompra ordenDeCompra);
        [OperationContract]
        Task<OrdenDeCompra> GetByIdOrdenDeCompraAsync(int id);
        [OperationContract]
        Task<IEnumerable<OrdenDeCompraInfoDTO>> GetAllOrdenDeCompraAsync();
        [OperationContract]
        Task DeleteOrdenDeCompraByIdAsync(int id);
    }
}
