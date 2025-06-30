using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Ubicaciones
{
    public int IdUbicacion { get; set; }

    public string? LugarUbicacion { get; set; }

    public virtual ICollection<DetalleOrdenEgreso> DetalleOrdenEgresos { get; set; } = new List<DetalleOrdenEgreso>();

    public virtual ICollection<DetalleOrdenIngreso> DetalleOrdenIngresos { get; set; } = new List<DetalleOrdenIngreso>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<Transferencias> TransferenciaUbicacionDestinos { get; set; } = new List<Transferencias>();

    public virtual ICollection<Transferencias> TransferenciaUbicacionOrigens { get; set; } = new List<Transferencias>();
}
