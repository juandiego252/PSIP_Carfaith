using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Carfaith.Dominio.Modelo.Entidades
{
    public partial class LoteProducto
    {
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public string CodigoLote { get; set; }

        public DateOnly FechaIngreso { get; set; }

        public DateOnly? FechaVencimiento { get; set; }

        public int CantidadActual { get; set; }

        public string Estado { get; set; }

        public virtual ICollection<DetalleOrdenEgreso> DetalleOrdenEgreso { get; set; } = new List<DetalleOrdenEgreso>();

        public virtual ICollection<DetalleOrdenIngreso> DetalleOrdenIngreso { get; set; } = new List<DetalleOrdenIngreso>();

        public virtual ICollection<DetalleTransferencia> DetalleTransferencia { get; set; } = new List<DetalleTransferencia>();

        public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompra{ get; set; } = new List<DetalleOrdenCompra>();

        [JsonIgnore]
        public virtual Producto? ProductoNavigation { get; set; }
    }
}
