using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IDetalleTransferenciaServicio
    {
        [OperationContract]
        Task AddDetalleTransferenciaAsync(DetalleTransferencia detalleTransferencia);

        [OperationContract]
        Task UpdateDetalleTransferenciaAsync(DetalleTransferencia detalleTransferencia);

        [OperationContract]
        Task DeleteDetalleTransferenciaAsync(int id);

        [OperationContract]
        Task<IEnumerable<DetalleTransferencia>> GetAllDetalleTransferenciaAsync();

        [OperationContract]
        Task<IEnumerable<DetalleTransferencia>> GetDetalleTransferenciaByIdAsync(int id);
    }
}
