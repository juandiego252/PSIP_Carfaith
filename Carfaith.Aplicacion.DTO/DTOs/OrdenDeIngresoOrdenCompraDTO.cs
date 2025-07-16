using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs
{
    public class OrdenDeIngresoOrdenCompraDTO
    {
        // Propiedad Orden de Ingreo
        public int IdOrdenIngreso { get; set; }
        public string? OrigenDeCompra { get; set; }

        // Propiedad Orden de Compra
        public string? NumeroOrdenCompra { get; set; }
        public string? ArchivoPdf { get; set; }
        // Propiedades del Proveedor
        public string? NombreProveedor { get; set; }

        public string? PaisOrigen { get; set; }

        public string? TipoProveedor { get; set; }

        public string? PersonaContacto { get; set; }
    }
}
