using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class DetalleOrdenEgreso
{
    public int IdDetalleOrdenEgreso { get; set; }

    public int? OrdenEgresoId { get; set; }

    public int? IdProductoProveedor { get; set; }

    public int? Cantidad { get; set; }

    public int? UbicacionId { get; set; }

    [JsonIgnore]
    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }
    [JsonIgnore]
    public virtual OrdenEgreso? OrdenEgreso { get; set; }
    [JsonIgnore]
    public virtual Ubicaciones? Ubicacion { get; set; }
}
