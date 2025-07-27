using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class StockProductoProveedorUbicacionDTO
    {
        // Propiedades del stock
        public int IdStock { get; set; }
        public int? Cantidad { get; set; }


        // Propiedades del producto
        public string? CodigoProducto { get; set; }
        public string? NombreProducto { get; set; }

        // Propiedades del proveedor
        public string? NombreProveedor { get; set; }
        public string? TipoProveedor { get; set; }

        // Propiedades de la ubicacion
        public string? LugarUbicacion { get; set; }
    }
}
