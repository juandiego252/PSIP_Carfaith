using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class DetalleOrdenCompra
{
    public int IdDetalleOrden { get; set; }

    public int? IdOrden { get; set; }

    public int? IdProductoProveedor { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual OrdenDeCompra? IdOrdenNavigation { get; set; }

    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }
}
