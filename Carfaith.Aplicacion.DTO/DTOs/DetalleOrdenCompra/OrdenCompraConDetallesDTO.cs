using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenCompra
{
    public class OrdenCompraConDetallesDTO
    {
        public int? IdOrden { get; set; }

        public string? NumeroOrden { get; set; }

        public int? IdProveedor { get; set; }

        public string? NombreProveedor { get; set; }

        public string? ArchivoPdf { get; set; }

        public string? Estado { get; set; }

        public DateOnly? FechaCreacion { get; set; }

        public DateOnly? FechaEstimadaEntrega { get; set; }

        public virtual List<OrdenCompraDetallesDTO> Detalles { get; set; } = new();
    }
}
