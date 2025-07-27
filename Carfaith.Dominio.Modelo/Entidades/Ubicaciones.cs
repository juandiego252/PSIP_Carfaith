using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Ubicaciones
{
    public int IdUbicacion { get; set; }

    public string? LugarUbicacion { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleOrdenEgreso> DetalleOrdenEgresos { get; set; } = new List<DetalleOrdenEgreso>();
    [JsonIgnore]
    public virtual ICollection<DetalleOrdenIngreso> DetalleOrdenIngresos { get; set; } = new List<DetalleOrdenIngreso>();
    [JsonIgnore]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    [JsonIgnore]
    public virtual ICollection<Transferencias> TransferenciaUbicacionDestinos { get; set; } = new List<Transferencias>();
    [JsonIgnore]
    public virtual ICollection<Transferencias> TransferenciaUbicacionOrigens { get; set; } = new List<Transferencias>();
}
