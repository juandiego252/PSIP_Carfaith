using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class ProductoProveedorDetalleDTO
    {
        public int IdProductoProveedor { get; set; }
        public string NombreProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProveedor { get; set; }
        public string PaisOrigen { get; set; }
        public string LineaProducto { get; set; }
    }
}
