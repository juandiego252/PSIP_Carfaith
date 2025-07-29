using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenCompra
{
    public class OrdenCompraDetallesDTO
    {
        public int? IdProductoProveedor { get; set; }

        public int? Cantidad { get; set; }

        public decimal? PrecioUnitario { get; set; }

    }
}
