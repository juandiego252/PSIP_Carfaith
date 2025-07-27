using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Aplicacion.DTO.DTOs.Transferencias
{
    public class DetalleTransferenciaInfoDTO
    {
        public int IdDetalleTransferencia { get; set; }
        public int? IdProductoProveedor { get; set; }
        public string NombreProducto { get; set; }
        public int? Cantidad { get; set; }
    }
}
