using Carfaith.Aplicacion.DTO;
using Carfaith.Aplicacion.DTO.DTOs.OrdenDeCompra;
using Carfaith.Dominio.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IPreciosHistoricosRepositorio : IRepositorio<PreciosHistoricos>
    {
        public Task<IEnumerable<PreciosHistoricosDTO>> GetPreciosHistoricosProductos();
    }
}
