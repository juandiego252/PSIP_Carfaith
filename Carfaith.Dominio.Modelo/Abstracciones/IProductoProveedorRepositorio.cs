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
        Task<IEnumerable<ProductoProveedorDetalleDTO>> GetProductoProveedorDetalleAsync();

        // Consulta N2
        Task<IEnumerable<ProveedorProductosDTO>> GetProveedoresConProductosAsync();
    }
}
