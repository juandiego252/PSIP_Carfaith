using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Stock
{
    public int IdStock { get; set; }

    public int? IdProductoProveedor { get; set; }

    public int? IdUbicacion { get; set; }

    public int? Cantidad { get; set; }

    [JsonIgnore]
    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }
    [JsonIgnore]
    public virtual Ubicaciones? IdUbicacionNavigation { get; set; }
}
