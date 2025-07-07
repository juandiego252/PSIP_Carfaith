using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class ProveedorConProductosDTO
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int TotalProductos { get; set; }
    }
}
