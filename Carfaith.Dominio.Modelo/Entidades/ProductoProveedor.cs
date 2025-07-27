using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class ProductoProveedor
{
    public int IdProductoProveedor { get; set; }

    public int? IdProducto { get; set; }

    public int? IdProveedor { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompras { get; set; } = new List<DetalleOrdenCompra>();
    [JsonIgnore]
    public virtual ICollection<DetalleOrdenEgreso> DetalleOrdenEgresos { get; set; } = new List<DetalleOrdenEgreso>();
    [JsonIgnore]
    public virtual ICollection<DetalleOrdenIngreso> DetalleOrdenIngresos { get; set; } = new List<DetalleOrdenIngreso>();
    [JsonIgnore]
    public virtual ICollection<DetalleTransferencia> DetalleTransferencia { get; set; } = new List<DetalleTransferencia>();
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
    [JsonIgnore]
    public virtual Proveedores? IdProveedorNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<PreciosHistoricos> PreciosHistoricos { get; set; } = new List<PreciosHistoricos>();
    [JsonIgnore]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
