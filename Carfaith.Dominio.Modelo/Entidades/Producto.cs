using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? CodigoProducto { get; set; }

    public string? Nombre { get; set; }

    public int? LineaDeProducto { get; set; }

    public virtual LineasDeProducto? LineaDeProductoNavigation { get; set; }

    public virtual ICollection<ProductoProveedor> ProductoProveedors { get; set; } = new List<ProductoProveedor>();
}
