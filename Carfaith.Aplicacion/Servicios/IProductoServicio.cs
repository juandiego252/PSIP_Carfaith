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
    public interface IProductoServicio
    {
        [OperationContract]
        Task AddProductoAsync(Producto producto);

        [OperationContract]
        Task UpdateProductoAsync(Producto producto);

        [OperationContract]
        Task<Producto> GetByIdProductoAsync(int id);

        [OperationContract]
        Task<IEnumerable<Producto>> GetAllProductoAsync();

        [OperationContract]
        Task DeleteProductoByIdAsync(int id);
    }
}
