using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class ProveedoresRepositorioImpl : RepositorioImpl<Proveedores>, IProveedoresRepositorio
    {
        private readonly CarfaithDbContext _context;
        public ProveedoresRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedores>> GetProveedoresPorNombreAsync(string nombre)
        {
            var proveedores = from p in _context.Proveedores
                              where p.NombreProveedor == nombre
                              select p;

            return await proveedores.ToListAsync();
        }

        public async Task<IEnumerable<Proveedores>> GetProveedoresPorPais(string paisOrigen)
        {
            var proveedores = from p in _context.Proveedores
                              where p.PaisOrigen == paisOrigen
                              select p;

            return await proveedores.ToListAsync();
        }

        public async Task<IEnumerable<Proveedores>> GetProveedoresPorRucAsync(string ruc)
        {
            var proveedores = from p in _context.Proveedores
                              where p.Ruc == ruc
                              select p;
            return await proveedores.ToListAsync();
        }

        public async Task<IEnumerable<Proveedores>> GetProveedoresPorTipoProveedor(string tipoProveedor)
        {
            var proveedores = from p in _context.Proveedores
                              where p.TipoProveedor == tipoProveedor
                              select p;

            return await proveedores.ToListAsync();
        }
    }
}
