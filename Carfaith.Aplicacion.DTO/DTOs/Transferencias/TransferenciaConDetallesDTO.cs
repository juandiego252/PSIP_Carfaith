using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.Transferencias
{
    public class TransferenciaConDetallesDTO
    {
        public int IdTransferencia { get; set; }
        public DateOnly? Fecha { get; set; }
        public int? UbicacionOrigenId { get; set; }
        public int? UbicacionDestinoId { get; set; }
        public string UbicacionOrigenNombre { get; set; }
        public string UbicacionDestinoNombre { get; set; }
        public List<DetalleTransferenciaInfoDTO> Detalles { get; set; } = new List<DetalleTransferenciaInfoDTO>();
    }
}
