using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class DetalleOrdenEgreso
{
    public int IdDetalleOrdenEgreso { get; set; }

    public int? OrdenEgresoId { get; set; }

    public int? IdProductoProveedor { get; set; }

    public int? Cantidad { get; set; }

    public int? UbicacionId { get; set; }

    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }

    public virtual OrdenEgreso? OrdenEgreso { get; set; }

    public virtual Ubicaciones? Ubicacion { get; set; }
}
