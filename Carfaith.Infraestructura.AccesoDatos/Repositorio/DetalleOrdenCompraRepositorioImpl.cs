using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Infraestructura.AccesoDatos.Repositorio
{
    public class DetalleOrdenCompraRepositorioImpl : RepositorioImpl<DetalleOrdenCompra>, IDetalleOrdenCompraRepositorio
    {
        public DetalleOrdenCompraRepositorioImpl(CarfaithDbContext context) : base(context)
        {
        }
    }
}
