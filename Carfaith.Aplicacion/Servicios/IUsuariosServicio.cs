using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Aplicacion.Servicios
{
    [ServiceContract]
    public interface IUsuariosServicio
    {
        [OperationContract]
        Task AddUsuariosAsync(Usuarios usuario);
        [OperationContract]
        Task UpdateUsuariosAsync(Usuarios usuario);
        [OperationContract]
        Task<Usuarios> GetByIdUsuariosAsync(int id);

        [OperationContract]
        Task<IEnumerable<Usuarios>> GetAllUsuariosAsync();

        [OperationContract]
        Task DeleteUsuariosByIdAsync(int id);

        [OperationContract]
        Task<bool> VerifyUsuariosAsync(string email, string password);
    }
}
