using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.Proveedor
{
    public class ProveedorDetalleDTO
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string PaisOrigen { get; set; }
        public string TipoProveedor { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string PersonaContacto { get; set; }
        public DateOnly? FechaRegistro { get; set; }
        public string Ruc { get; set; }
        public string Direccion { get; set; }
        public bool? Estado { get; set; }
        public int TotalProductos { get; set; }
        public int TotalOrdenes { get; set; }

    }
}
