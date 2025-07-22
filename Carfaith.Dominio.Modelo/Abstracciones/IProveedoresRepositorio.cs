using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.Proveedor;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IProveedoresRepositorio : IRepositorio<Proveedores>
    {
        Task<IEnumerable<Proveedores>> GetProveedoresPorNombreAsync(string nombre);
        Task<IEnumerable<Proveedores>> GetProveedoresPorTipoProveedor(string tipoProveedor);
        Task<IEnumerable<Proveedores>> GetProveedoresPorPais(string paisOrigen);

        Task<IEnumerable<Proveedores>> GetProveedoresPorRucAsync(string ruc);

        Task<IEnumerable<ProveedorDetalleDTO>> GetProveedoresConDetallesAsync();
    }
}
