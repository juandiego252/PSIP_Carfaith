using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class ProductoPorLineaProveedorDTO
    {
        public string NombreLinea { get; set; }
        public int TotalProductos { get; set; }
        public int TotalProveedores { get; set; }
        public List<ProductoProveedorBasicoDTO> ProductosProveedores { get; set; } = new List<ProductoProveedorBasicoDTO>();
    }
}
