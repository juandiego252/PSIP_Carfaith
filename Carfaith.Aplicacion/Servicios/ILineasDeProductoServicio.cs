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
    public interface ILineasDeProductoServicio
    {
        // Operaciones CRUD
        [OperationContract]
        Task AddLineasDeProductoAsync(LineasDeProducto lineasDeProducto);

        [OperationContract]
        Task UpdateLineasDeProductoAsync(LineasDeProducto lineasDeProducto);

        [OperationContract]
        Task<LineasDeProducto> GetByIdLineasDeProductoAsync(int id);
        
        [OperationContract]
        Task<IEnumerable<LineasDeProducto>> GetAllLineasDeProductoAsync();

        [OperationContract]
        Task DeleteLineasDeProductoByIdAsync(int id);
    }
}
