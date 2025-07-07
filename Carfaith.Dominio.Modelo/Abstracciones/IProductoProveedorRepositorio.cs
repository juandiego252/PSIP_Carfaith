using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IProductoProveedorRepositorio : IRepositorio<ProductoProveedor>
    {
        // Consulta N1
        public Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductoProveedorDetalleAsync();

        // Consulta N2
        public Task<IEnumerable<ProveedorProductosDTO>> GetProveedoresConProductosAsync();

        // Consulta N3
        Task<IEnumerable<LineaProductoProveedoresDTO>> GetLineasConProveedoresAsync();

        // Consulta N4
        Task<IEnumerable<ProductoPorLineaProveedorDTO>> GetProductosPorLineaProveedorAsync();


        // Consulta N5
        Task<IEnumerable<ProveedorProductosPorPaisDTO>> GetProveedorProductosPorPaisAsync();


        // Consulta N6
        Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductosProveedoresPorLineaAsync(int idLinea);


        // Consulta N7
        Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductosProveedoresPorPaisAsync(string paisOrigen);
    }
}
