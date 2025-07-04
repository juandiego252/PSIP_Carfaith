using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IProveedoresRepositorio : IRepositorio<Proveedores>
    {
        public Task<IEnumerable<Proveedores>> GetProveedoresPorNombreAsync(string nombre);
        public Task<IEnumerable<Proveedores>> GetProveedoresPorTipoProveedor(string tipoProveedor);
        public Task<IEnumerable<Proveedores>> GetProveedoresPorPais(string paisOrigen);

        public Task<IEnumerable<Proveedores>> GetProveedoresPorRucAsync(string ruc);
    }
}
