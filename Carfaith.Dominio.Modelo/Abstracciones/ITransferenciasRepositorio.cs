using Carfaith.Dominio.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface ITransferenciasRepositorio : IRepositorio<Transferencias>
    {
        Task<IEnumerable<Transferencias>> GetAllTransferenciasWithRelationsAsync();
        Task<Transferencias> GetTransferenciaByIdWithRelationsAsync(int idTransferencia);
    }
}
