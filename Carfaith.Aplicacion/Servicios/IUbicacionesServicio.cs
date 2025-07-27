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
    public interface IUbicacionesServicio
    {
        [OperationContract]
        Task AddUbicacionesAsync(Ubicaciones ubicaciones);

        [OperationContract]
        Task UpdateUbicacionesAsync(Ubicaciones ubicaciones);

        [OperationContract]
        Task DeleteUbicacionesAsync(int id);

        [OperationContract]
        Task<IEnumerable<Ubicaciones>> GetAllUbicacionesAsync();

        [OperationContract]
        Task<Ubicaciones> GetUbicacionesById(int id);
    }
}
