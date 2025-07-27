using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenIngreso;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IOrdenIngresoStockServicio
    {
        [OperationContract]
        Task<int> CrearOrdenIngresoConDetalles(OrdenIngresoConDetallesDTO ordenIngresoConDetallesDTO);

        [OperationContract]
        Task<int> EditarOrdenIngresoConDetalles(OrdenIngresoConDetallesDTO ordenIngresoConDetallesDTO);

        [OperationContract]
        Task<IEnumerable<OrdenIngresoConDetallesDTO>> GetOrdenIngresoConDetalles();

        [OperationContract]
        Task EliminarOrdenIngresoConDetalles(int idOrdenIngreso);
    }
}
