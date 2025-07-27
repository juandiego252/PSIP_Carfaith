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
    public interface IOrdenDeIngresoServicio
    {
        // CRUD para OrdenDeIngreso
        [OperationContract]
        Task AddOrdenDeIngresoAsync(OrdenDeIngreso ordenDeIngreso);
        [OperationContract]
        Task UpdateOrdenDeIngresoAsync(OrdenDeIngreso ordenDeIngreso);
        [OperationContract]
        Task<OrdenDeIngreso> GetByIdOrdenDeIngresoAsync(int id);
        [OperationContract]
        Task<IEnumerable<OrdenDeIngreso>> GetAllOrdenDeIngresoAsync();
        [OperationContract]
        Task DeleteOrdenDeIngresoByIdAsync(int id);

        // Consultas
        [OperationContract]
        Task<IEnumerable<OrdenDeIngresoOrdenCompraDTO>> OrdenDeIngresoOrdenCompraDTOs();
    }
}
