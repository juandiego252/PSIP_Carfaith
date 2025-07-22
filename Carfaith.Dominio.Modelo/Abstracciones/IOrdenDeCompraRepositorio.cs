using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.OrdenDeCompra;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IOrdenDeCompraRepositorio : IRepositorio<OrdenDeCompra>
    {
        public Task<List<OrdenDeCompraInfoDTO>> GetOrdenesDeCompraProveedor();
    }
}
