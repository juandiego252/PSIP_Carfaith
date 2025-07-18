using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class StockProductoProveedorUbicacionDTO
    {
        // Propiedades de Stock
        public int IdStock { get; set; }
        public int? Cantidad { get; set; }

        // Propiedades de Producto
        public string? CodigoProducto { get; set; }
        public string? NombreProducto { get; set; }

        // Propiedades de Proveedor

        public string? NombreProveedor { get; set; }
        public string? TipoProveedor { get; set; }

        // Propiedades de Ubicacion
        public string? LugarUbicacion { get; set; }

    }
}
