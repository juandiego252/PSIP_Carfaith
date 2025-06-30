using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class DetalleTransferencia
{
    public int IdDetalleTransferencia { get; set; }

    public int? IdTransferencia { get; set; }

    public int? IdProductoProveedor { get; set; }

    public int? Cantidad { get; set; }

    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }

    public virtual Transferencias? IdTransferenciaNavigation { get; set; }
}
