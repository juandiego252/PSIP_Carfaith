using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.Proveedor;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IProveedoresServicio
    {
        // Operaciones CRUD
        [OperationContract]
        Task AddProveedoresAsync(Proveedores proveedores);
        [OperationContract]
        Task UpdateProveedoresAsync(Proveedores proveedores);
        [OperationContract]
        Task<Proveedores> GetByIdProveedoresAsync(int id);

        [OperationContract]
        Task<IEnumerable<Proveedores>> GetAllProveedoresAsync();

        [OperationContract]
        Task DeleteProveedoresByIdAsync(int id);

        // Consultas
        [OperationContract]
        Task<IEnumerable<Proveedores>> GetProveedoresPorNombreAsync(string nombre);
        [OperationContract]
        Task<IEnumerable<Proveedores>> GetProveedoresPorTipoProveedor(string tipoProveedor);
        [OperationContract]
        Task<IEnumerable<Proveedores>> GetProveedoresPorPais(string paisOrigen);

        [OperationContract]
        Task<IEnumerable<ProveedorDetalleDTO>> GetProveedoresConDetallesAsync();
    }
}
