using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Transferencias
{
    public int IdTransferencia { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? UbicacionOrigenId { get; set; }

    public int? UbicacionDestinoId { get; set; }
    [JsonIgnore]
    public virtual ICollection<DetalleTransferencia> DetalleTransferencia { get; set; } = new List<DetalleTransferencia>();
    [JsonIgnore]
    public virtual Ubicaciones? UbicacionDestino { get; set; }
    [JsonIgnore]
    public virtual Ubicaciones? UbicacionOrigen { get; set; }
}
