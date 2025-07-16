using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class OrdenDeCompra
{
    public int IdOrden { get; set; }

    public string? NumeroOrden { get; set; }

    public int? IdProveedor { get; set; }

    public string? ArchivoPdf { get; set; }

    public string? Estado { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public DateOnly? FechaEstimadaEntrega { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompras { get; set; } = new List<DetalleOrdenCompra>();
    [JsonIgnore]
    public virtual Proveedores? IdProveedorNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrdenDeIngreso> OrdenDeIngresos { get; set; } = new List<OrdenDeIngreso>();
}
