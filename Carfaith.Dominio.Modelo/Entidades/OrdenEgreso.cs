using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class OrdenEgreso
{
    public int IdOrdenEgreso { get; set; }

    public string? TipoEgreso { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Destino { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleOrdenEgreso> DetalleOrdenEgresos { get; set; } = new List<DetalleOrdenEgreso>();
}
