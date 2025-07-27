using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Carfaith.Aplicacion.DTO.DTOs.OrdenDeCompra
{
    public class OrdenDeCompraInfoDTO
    {
        public int idOrden { get; set; }
        public string? numeroOrden { get; set; }
        public int? idProveedor { get; set; }
        public string? nombreProveedor { get; set; }
        public string? archivoPdf { get; set; }
        public string? estado { get; set; }
        public DateOnly? fechaCreacion { get; set; }
        public DateOnly? fechaEstimadaEntrega { get; set; }
    }
}
