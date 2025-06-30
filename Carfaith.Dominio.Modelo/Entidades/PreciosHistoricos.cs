using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class PreciosHistoricos
{
    public int IdPreciosHistoricos { get; set; }

    public int? IdProductoProveedor { get; set; }

    public decimal? Precio { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFinalizacion { get; set; }

    public virtual ProductoProveedor? IdProductoProveedorNavigation { get; set; }
}
