using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class ProveedorProductosPorPaisDTO
    {
        public string PaisOrigen { get; set; }
        public int TotalProveedores { get; set; }
        public List<ProveedorConProductosDTO> Proveedores { get; set; } = new List<ProveedorConProductosDTO>();
    }
}
