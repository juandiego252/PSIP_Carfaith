using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class DetalleTransferencia
{
    public int IdDetalleTransferencia { get; set; }

    public int? IdTransferencia { get; set; }

    public int? IdProductoProveedor { get; set; }

    public int? Cantidad { get; set; }

    [JsonIgnore]
    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }
    [JsonIgnore]
    public virtual Transferencias? IdTransferenciaNavigation { get; set; }
}
