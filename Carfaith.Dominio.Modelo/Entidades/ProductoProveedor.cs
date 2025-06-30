using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class ProductoProveedor
{
    public int IdProductoProveedor { get; set; }

    public int? IdProducto { get; set; }

    public int? IdProveedor { get; set; }

    public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompras { get; set; } = new List<DetalleOrdenCompra>();

    public virtual ICollection<DetalleOrdenEgreso> DetalleOrdenEgresos { get; set; } = new List<DetalleOrdenEgreso>();

    public virtual ICollection<DetalleOrdenIngreso> DetalleOrdenIngresos { get; set; } = new List<DetalleOrdenIngreso>();

    public virtual ICollection<DetalleTransferencia> DetalleTransferencia { get; set; } = new List<DetalleTransferencia>();

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Proveedores? IdProveedorNavigation { get; set; }

    public virtual ICollection<PreciosHistoricos> PreciosHistoricos { get; set; } = new List<PreciosHistoricos>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
