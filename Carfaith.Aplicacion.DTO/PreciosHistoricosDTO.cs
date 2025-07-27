using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO
{
    public class PreciosHistoricosDTO
    {
        public int IdPreciosHistoricos { get; set; }

        public string? CodigoProducto { get; set; }
        
        public string? NombreProducto { get; set; }

        public string? LineaProducto { get; set; }

        public string? NombreProveedor { get; set; }

        public string? TipoProveedor { get; set; }

        public decimal? Precio { get; set; }

        public DateOnly? FechaInicio { get; set; }

        public DateOnly? FechaFinalizacion { get; set; }
    }
}
