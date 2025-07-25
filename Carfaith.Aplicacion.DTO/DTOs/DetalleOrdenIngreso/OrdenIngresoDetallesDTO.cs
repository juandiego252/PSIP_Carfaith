using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenIngreso
{
    public class OrdenIngresoDetallesDTO
    {
        public int? IdProductoProveedor { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public int? UbicacionId { get; set; }
        public string? TipoIngreso { get; set; }
        public string? NumeroLote { get; set; }
    }
}
