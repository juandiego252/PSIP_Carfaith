using System;
using System.Collections.Generic;

namespace Carfaith.Dominio.Modelo.Entidades;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Email { get; set; }

    public string? Contraseña { get; set; }
}
