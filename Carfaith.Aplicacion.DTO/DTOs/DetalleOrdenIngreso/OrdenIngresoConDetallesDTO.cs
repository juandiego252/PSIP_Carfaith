using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenIngreso
{
    public class OrdenIngresoConDetallesDTO
    {
        public int? IdOrdenCompra { get; set; }
        public string? OrigenDeCompra { get; set; }
        public DateOnly? Fecha { get; set; }
        public string? Estado { get; set; }
        public List<OrdenIngresoDetallesDTO> Detalles { get; set; } = new();
    }
}
