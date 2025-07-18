﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Dominio.Modelo.Entidades;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        public Task<bool> IsCodigoProductoUnique(string codigoProducto);
    }
}
