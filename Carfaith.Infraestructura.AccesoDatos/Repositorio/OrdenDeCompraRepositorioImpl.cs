using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class OrdenDeCompraRepositorioImpl : RepositorioImpl<OrdenDeCompra>, IOrdenDeCompraRepositorio
    {
        private readonly CarfaithDbContext _context;
        public OrdenDeCompraRepositorioImpl(CarfaithDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
