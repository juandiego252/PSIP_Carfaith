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
    public class DetalleOrdenIngresoRepositorioImpl : RepositorioImpl<DetalleOrdenIngreso>, IDetalleOrdenIngresoRepositorio
    {
        public DetalleOrdenIngresoRepositorioImpl(CarfaithDbContext context) : base(context)
        {
        }
    }
}
