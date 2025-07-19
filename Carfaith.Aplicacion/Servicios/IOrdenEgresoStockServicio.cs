using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IOrdenEgresoStockServicio
    {
        [OperationContract]
        Task<int> CrearOrdenEgresoConDetalles(OrdenEgresoConDetallesDTO ordenEgresoConDetallesDTO);
    }
}
