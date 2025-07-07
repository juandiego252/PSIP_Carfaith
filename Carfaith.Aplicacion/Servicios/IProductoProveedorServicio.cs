using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IProductoProveedorServicio
    {
        // Operaciones CRUD
        [OperationContract]
        Task AddProductoProveedorAsync(ProductoProveedor productoProveedor);

        [OperationContract]
        Task UpdateProductoProveedorAsync(ProductoProveedor productoProveedor);

        [OperationContract]
        Task DeleteProductoProveedorAsync(int idProductoProveedor);

        [OperationContract]
        Task<ProductoProveedor> GetByIdProductoProveedorAsync(int idProductoProveedor);

        [OperationContract]
        Task<IEnumerable<ProductoProveedor>> GetAllProductoProveedorAsync();

        // Consultas 

        [OperationContract]
        Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductoProveedorDetallesAsync();

        [OperationContract]
        public Task<IEnumerable<ProveedorProductosDTO>> GetProveedoresConProductosAsync();
    }
}
