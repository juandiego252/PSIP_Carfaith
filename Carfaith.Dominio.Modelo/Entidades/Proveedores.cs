using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Proveedores
{
    public int IdProveedor { get; set; }

    public string? NombreProveedor { get; set; }

    public string? PaisOrigen { get; set; }

    public string? TipoProveedor { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? PersonaContacto { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public string? Ruc { get; set; }

    public string? Direccion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<OrdenDeCompra> OrdenDeCompras { get; set; } = new List<OrdenDeCompra>();

    public virtual ICollection<ProductoProveedor> ProductoProveedors { get; set; } = new List<ProductoProveedor>();
}
