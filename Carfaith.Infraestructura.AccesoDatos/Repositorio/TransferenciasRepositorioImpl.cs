using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class TransferenciasRepositorioImpl : RepositorioImpl<Transferencias>, ITransferenciasRepositorio
    {
        private readonly CarfaithDbContext _context;
        public TransferenciasRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }

        // En TransferenciasRepositorioImpl.cs
        public async Task<IEnumerable<Transferencias>> GetAllTransferenciasWithRelationsAsync()
        {
            return await _context.Transferencias
                .Include(t => t.UbicacionOrigen)
                .Include(t => t.UbicacionDestino)
                .ToListAsync();
        }

        public async Task<Transferencias> GetTransferenciaByIdWithRelationsAsync(int idTransferencia)
        {
            return await _context.Transferencias
                .Include(t => t.UbicacionOrigen)
                .Include(t => t.UbicacionDestino)
                .FirstOrDefaultAsync(t => t.IdTransferencia == idTransferencia);
        }
    }
}
