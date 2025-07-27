using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IOrdenEgresoStockServicio
    {
        [OperationContract]
        Task<int> CrearOrdenEgresoConDetalles(OrdenEgresoConDetallesDTO ordenEgresoConDetallesDTO);

        [OperationContract]
        Task<int> EditarOrdenEgresoConDetalles(OrdenEgresoConDetallesDTO OrdenEgresoConDetallesDTO);

        [OperationContract]
        Task<IEnumerable<OrdenEgresoConDetallesDTO>> GetOrdenEgresoConDetalles();

        [OperationContract]
        Task EliminarOrdenEgresoConDetalles(int idOrdenEgreso);
    }
}
