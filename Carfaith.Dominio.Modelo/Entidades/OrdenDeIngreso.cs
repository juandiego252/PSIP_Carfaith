using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class OrdenDeIngreso
{
    public int IdOrdenIngreso { get; set; }

    public int? IdOrdenCompra { get; set; }

    public string? OrigenDeCompra { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleOrdenIngreso> DetalleOrdenIngresos { get; set; } = new List<DetalleOrdenIngreso>();

    public virtual OrdenDeCompra? IdOrdenCompraNavigation { get; set; }
}
