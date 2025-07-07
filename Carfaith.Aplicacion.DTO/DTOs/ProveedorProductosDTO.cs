using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class ProveedorProductosDTO
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string PaisOrigen { get; set; }
        public string Ruc { get; set; }
        public int TotalProductos { get; set; }
        public List<ProductoResumenDTO> Productos { get; set; } = new List<ProductoResumenDTO>();
    }
}
