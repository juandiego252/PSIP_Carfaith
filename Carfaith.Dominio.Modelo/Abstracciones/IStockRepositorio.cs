﻿using Carfaith.Aplicacion.DTO.DTOs;
using Carfaith.Dominio.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfaith.Dominio.Modelo.Abstracciones
{
    public interface IStockRepositorio : IRepositorio<Stock>
    {
        Task<IEnumerable<StockProductoProveedorUbicacionDTO>> GetStockProductoProveedorUbicacionDto();
        Task<Stock> GetStockByProductoProveedorYUbicacionAsync(int idProductoProveedor, int idUbicacion);
        Task<Stock> ActualizarStockPorTransferenciaAsync(int idProductoProveedor, int idUbicacion, int cantidad);
    }
}
