using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class AsociacionMasivaDTO
    {
        public int IdProducto { get; set; }
        public List<int> IdsProveedores { get; set; } = new List<int>();
    }
}
