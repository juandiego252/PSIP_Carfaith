using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class ProductoResumenDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string CodigoProducto { get; set; }
        public string LineaProducto { get; set; }
    }
}
