using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.Transferencias;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface ITransferenciaStockServicio
    {
        [OperationContract]
        Task<int> ProcesarTransferenciaCompleta(TransferenciaStockDTO transferenciaDTO);

        [OperationContract]
        Task<IEnumerable<TransferenciaConDetallesDTO>> GetTransferenciasConDetalles();

        [OperationContract]
        Task<TransferenciaConDetallesDTO> GetTransferenciaConDetallesById(int idTransferencia);
    }
}
