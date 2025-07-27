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
    public interface IDetalleOrdenIngresoServicio
    {
        [OperationContract]
        Task AddDetalleOrdenIngresoAsync(DetalleOrdenIngreso detalleOrdenIngreso);

        [OperationContract]
        Task UpdateDetalleOrdenIngresoAsync(DetalleOrdenIngreso detalleOrdenIngreso);

        [OperationContract]
        Task<DetalleOrdenIngreso> GetByIdDetalleOrdenIngresoAsync(int id);

        [OperationContract]
        Task<IEnumerable<DetalleOrdenIngreso>> GetAllDetalleOrdenIngresoAsync();

        [OperationContract]
        Task DeleteDetalleOrdenIngresoByIdAsync(int id);
    }
}
