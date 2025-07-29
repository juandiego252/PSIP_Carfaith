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
    public interface IDetalleOrdenCompraServicio
    {
        [OperationContract]
        Task AddDetalleOrdenCompra(DetalleOrdenCompra detalleOrdenCompra);

        [OperationContract]
        Task<IEnumerable<DetalleOrdenCompra>> GetAllDetallesOrdenCompraAsync();

        [OperationContract]
        Task<DetalleOrdenCompra> GetDetalleOrdenCompraById(int id);

        [OperationContract]
        Task UpdateDetalleOrdenCompra(DetalleOrdenCompra detalleOrdenCompra);

        [OperationContract]
        Task DeleteDetalleOrdenCompra(int id);
    }
}
