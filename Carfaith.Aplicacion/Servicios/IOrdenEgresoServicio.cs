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
    public interface IOrdenEgresoServicio
    {
        [OperationContract]
        Task AddOrdenEgresoAsync(OrdenEgreso ordenEgreso);

        [OperationContract]
        Task UpdateOrdenEgresoAsync(OrdenEgreso ordenEgreso);

        [OperationContract]
        Task DeleteOrdenEgresoAsync(int id);

        [OperationContract]
        Task<OrdenEgreso> GetOrdenEgresoByIdAsync(int id);

        [OperationContract]
        Task<IEnumerable<OrdenEgreso>> GetAllOrdenEgresosAsync();
    }
}
