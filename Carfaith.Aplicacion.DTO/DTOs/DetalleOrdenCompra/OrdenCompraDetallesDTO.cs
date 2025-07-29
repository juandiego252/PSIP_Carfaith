using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenCompra
{
<<<<<<<< HEAD:Carfaith.Aplicacion.DTO/DTOs/DetalleOrdenEgreso/OrdenEgresoDetallesDTO.cs
    public class OrdenEgresoDetallesDTO
========
    public class OrdenCompraDetallesDTO
>>>>>>>> main:Carfaith.Aplicacion.DTO/DTOs/DetalleOrdenCompra/OrdenCompraDetallesDTO.cs
    {
        public int? IdProductoProveedor { get; set; }

        public int? Cantidad { get; set; }

<<<<<<<< HEAD:Carfaith.Aplicacion.DTO/DTOs/DetalleOrdenEgreso/OrdenEgresoDetallesDTO.cs
        public string? TipoEgreso { get; set; }

        public int? UbicacionId { get; set; }
========
        public decimal? PrecioUnitario { get; set; }

>>>>>>>> main:Carfaith.Aplicacion.DTO/DTOs/DetalleOrdenCompra/OrdenCompraDetallesDTO.cs
    }
}
