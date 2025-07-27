using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class DetalleOrdenIngreso
{
    public int IdDetalleOrdenIngreso { get; set; }

    public int? OrdenIngresoId { get; set; }

    public int? IdProductoProveedor { get; set; }

    public int? IdLoteProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public int? UbicacionId { get; set; }

    public string? TipoIngreso { get; set; }

    public string? NumeroLote { get; set; }

    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }

    public virtual OrdenDeIngreso? OrdenIngreso { get; set; }

    public virtual Ubicaciones? Ubicacion { get; set; }

    [JsonIgnore]
    public virtual LoteProducto? LoteProducto { get; set; }
}
