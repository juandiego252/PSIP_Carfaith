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
    public class DetalleTransferenciaRepositorioImpl : RepositorioImpl<DetalleTransferencia>, IDetalleTransferenciaRepositorio
    {
        private readonly CarfaithDbContext _context;
        public DetalleTransferenciaRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleTransferencia>> GetDetallesByTransferenciaIdAsync(int idTransferencia)
        {
            return await _context.DetalleTransferencia.
                Where(dt => dt.IdTransferencia == idTransferencia)
                .Include(dt => dt.IdProductoProveedorNavigation).ThenInclude(pp => pp.IdProductoNavigation).
                ToListAsync();
        }
    }
}
