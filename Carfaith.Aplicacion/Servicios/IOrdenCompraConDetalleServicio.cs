using Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IOrdenCompraConDetalleServicio
    {
        [OperationContract]
        Task<int> CrearOrdenCompraConDetalles(OrdenCompraConDetallesDTO ordenCompraConDetallesDTO);

        [OperationContract]
        Task<int> EditarOrdenCompraConDetalles(OrdenCompraConDetallesDTO ordenCompraConDetallesDTO);

        [OperationContract]
        Task<IEnumerable<OrdenCompraConDetallesDTO>> GetOrdenCompraConDetalles();

        [OperationContract]
        Task EliminarOrdenCompraConDetalles(int idOrdenCompra);
    }
}
