using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.DetalleOrdenEgreso
{
    public class OrdenEgresoDTO
    {
        public int IdOrdenEgreso { get; set; }
        public string? TipoEgreso { get; set; }
        public DateOnly? Fecha { get; set; }
        public string? Destino { get; set; }
        public string? Estado { get; set; }
        public List<DetalleOrdenEgresoDTO> Detalles { get; set; } = new List<DetalleOrdenEgresoDTO>();

    }
}
