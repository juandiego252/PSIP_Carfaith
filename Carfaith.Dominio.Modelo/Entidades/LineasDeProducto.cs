using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class LineasDeProducto
{
    public int IdLinea { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
