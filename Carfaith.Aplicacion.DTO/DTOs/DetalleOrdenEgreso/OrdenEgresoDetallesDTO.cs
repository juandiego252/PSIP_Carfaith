using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso
{
    public class OrdenEgresoDetallesDTO
    {
        public int? IdProductoProveedor { get; set; }

        public int? Cantidad { get; set; }

        public int? UbicacionId { get; set; }
    }
}
