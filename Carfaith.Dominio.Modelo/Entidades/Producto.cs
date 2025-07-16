using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Producto
{
    
    public int IdProducto { get; set; }

    public string? CodigoProducto { get; set; }

    public string? Nombre { get; set; }

    public int? LineaDeProducto { get; set; }

    [JsonIgnore]
    public virtual LineasDeProducto? LineaDeProductoNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProductoProveedor> ProductoProveedors { get; set; } = new List<ProductoProveedor>();
}
