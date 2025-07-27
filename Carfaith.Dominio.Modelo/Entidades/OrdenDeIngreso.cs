using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class OrdenDeIngreso
{
    public int IdOrdenIngreso { get; set; }

    public int? IdOrdenCompra { get; set; }

    public string? OrigenDeCompra { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleOrdenIngreso> DetalleOrdenIngresos { get; set; } = new List<DetalleOrdenIngreso>();
    [JsonIgnore]
    public virtual OrdenDeCompra? IdOrdenCompraNavigation { get; set; }
}
