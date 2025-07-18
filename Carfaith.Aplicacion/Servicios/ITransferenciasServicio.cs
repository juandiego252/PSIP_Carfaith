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
    public interface ITransferenciasServicio
    {
        [OperationContract]
        Task AddTransferenciasAsync(Transferencias transferencias);

        [OperationContract]
        Task UpdateTransferenciasAsync(Transferencias transferencias);

        [OperationContract]
        Task DeleteTransferenciasAsync(int id);

        [OperationContract]
        Task<IEnumerable<Transferencias>> GetAllTransferenciasAsync();

        [OperationContract]
        Task<Transferencias> GetTransferenciasById(int id);


        // Consultas
        [OperationContract]
        Task<IEnumerable<Transferencias>> GetAllTransferenciasWithRelationsAsync();

        [OperationContract]
        Task<Transferencias> GetTransferenciaByIdWithRelationsAsync(int id);
    }
}
