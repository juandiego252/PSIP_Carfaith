using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IDetalleOrdenEgresoServicio
    {
        [OperationContract]
        Task AddDetalleOrdenEgreso(DetalleOrdenEgreso detalleOrdenEgreso);

        /* [OperationContract]
        Task<List<DetalleOrdenEgreso>> GetDetallesOrdenEgresoByOrdenId(int ordenEgresoId); */

        [OperationContract]
        Task<IEnumerable<DetalleOrdenEgreso>> GetAllDetallesOrdenEgresoAsync();

        [OperationContract]
        Task<DetalleOrdenEgreso> GetDetalleOrdenEgresoById(int detalleOrdenEgresoId);

        [OperationContract]
        Task UpdateDetalleOrdenEgreso(DetalleOrdenEgreso detalleOrdenEgreso);

        [OperationContract]
        Task DeleteDetalleOrdenEgreso(int detalleOrdenEgresoId);
    }
}
