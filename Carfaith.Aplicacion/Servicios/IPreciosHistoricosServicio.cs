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
    public interface IPreciosHistoricosServicio
    {
        [OperationContract]
        Task AddPreciosHistoricosAsync(PreciosHistoricos preciosHistoricos);

        [OperationContract]
        Task UpdatePreciosHistoricosAsync(PreciosHistoricos preciosHistoricos);

        [OperationContract]
        Task<PreciosHistoricos> GetByIdPreciosHistoricosAsync(int id);

        [OperationContract]
        Task<IEnumerable<PreciosHistoricos>> GetAllPreciosHistoricosAsync();

        [OperationContract]
        Task DeletePreciosHistoricosByIdAsync(int id);
    }
}
