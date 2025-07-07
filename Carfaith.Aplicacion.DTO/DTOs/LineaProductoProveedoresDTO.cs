using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class LineaProductoProveedoresDTO
    {
        public int IdLinea { get; set; }
        public string NombreLinea { get; set; }
        public int TotalProductos { get; set; }
        public int TotalProveedores { get; set; }
        public List<ProveedorResumenDTO> Proveedores { get; set; } = new List<ProveedorResumenDTO>();
    }
}
