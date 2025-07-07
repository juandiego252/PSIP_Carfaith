using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class LineasDeProductoServicioImpl : ILineasDeProductoServicio
    {
        private readonly ILineasDeProductoRepositorio _lineasDeProductoRepositorio;

        public LineasDeProductoServicioImpl(CarfaithDbContext _context)
        {
            _lineasDeProductoRepositorio = new LineasDeProductoRepositorioImpl(_context);
        }
        public async Task AddLineasDeProductoAsync(LineasDeProducto lineasDeProducto)
        {
            await _lineasDeProductoRepositorio.AddAsync(lineasDeProducto);
        }

        public async Task DeleteLineasDeProductoByIdAsync(int id)
        {
            await _lineasDeProductoRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<LineasDeProducto>> GetAllLineasDeProductoAsync()
        {
            return await _lineasDeProductoRepositorio.GetAllAsync();
        }

        public async Task<LineasDeProducto> GetByIdLineasDeProductoAsync(int id)
        {
            return await _lineasDeProductoRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateLineasDeProductoAsync(LineasDeProducto lineasDeProducto)
        {
            await _lineasDeProductoRepositorio.UpdateAsync(lineasDeProducto);
        }
    }
}
