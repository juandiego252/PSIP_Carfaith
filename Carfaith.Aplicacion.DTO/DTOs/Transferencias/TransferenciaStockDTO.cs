using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.Transferencias
{
    public class TransferenciaStockDTO
    {
        public DateOnly? Fecha { get; set; }
        public int? UbicacionOrigenId { get; set; }
        public int? UbicacionDestinoId { get; set; }
        public List<DetalleTransferenciaDTO> Detalles { get; set; } = new List<DetalleTransferenciaDTO>();
    }
}
