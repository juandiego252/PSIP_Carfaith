﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfaith.Aplicacion.DTO.DTOs.OrdenDeCompra;
using Carfaith.Aplicacion.Servicios;
using Carfaith.Dominio.Modelo.Abstracciones;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Carfaith.Infraestructura.AccesoDatos.Repositorio;

namespace Carfaith.Aplicacion.ServiciosImpl
{
    public class OrdenDeCompraServicioImpl : IOrdenDeCompraServicio
    {
        private readonly IOrdenDeCompraRepositorio _ordenDeCompraRepositorio;

        public OrdenDeCompraServicioImpl(CarfaithDbContext context)
        {
            _ordenDeCompraRepositorio = new OrdenDeCompraRepositorioImpl(context);
        }
        public async Task AddOrdenDeCompraAsync(OrdenDeCompra ordenDeCompra)
        {
            if (ordenDeCompra == null)
            {
                throw new ArgumentNullException(nameof(ordenDeCompra), "La orden de compra no puede estar vacia.");
            }
            if (ordenDeCompra.IdProveedor == null)
            {
                throw new ArgumentException("En proveedor no puede estar vacio.", nameof(ordenDeCompra));
            }

            if (string.IsNullOrEmpty(ordenDeCompra.NumeroOrden))
            {
                ordenDeCompra.NumeroOrden = await GenerarNumeroOrdenAsync();
            }


            if (!string.IsNullOrEmpty(ordenDeCompra.NumeroOrden) && !await _ordenDeCompraRepositorio.IsCodigoOrdenUnique(ordenDeCompra.NumeroOrden))
            {
                throw new ArgumentException($"El número de orden {ordenDeCompra.NumeroOrden} ya se encuentra registrado", nameof(ordenDeCompra));
            }

            await _ordenDeCompraRepositorio.AddAsync(ordenDeCompra);
        }

        private async Task<string> GenerarNumeroOrdenAsync()
        {
            // Obtener todos los productos para encontrar el último código
            var ordenesCompra = await _ordenDeCompraRepositorio.GetAllAsync();

            // Filtrar solo los códigos con formato PROD-XXXX
            var codigosExistentes = ordenesCompra
                .Where(od => od.NumeroOrden != null && od.NumeroOrden.StartsWith("ORDN-"))
                .Select(od => od.NumeroOrden)
                .ToList();

            // Encontrar el número más alto actual
            int maxNumero = 0;
            foreach (var codigo in codigosExistentes)
            {
                if (int.TryParse(codigo.Substring(5), out int numero))
                {
                    if (numero > maxNumero)
                        maxNumero = numero;
                }
            }

            // Generar el siguiente número
            int siguienteNumero = maxNumero + 1;

            // Formatear el nuevo código con ceros a la izquierda (PROD-0001)
            return $"ORDN-{siguienteNumero:D4}";
        }

        public async Task DeleteOrdenDeCompraByIdAsync(int id)
        {
            await _ordenDeCompraRepositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrdenDeCompraInfoDTO>> GetAllOrdenDeCompraAsync()
        {
            /*return await _ordenDeCompraRepositorio.GetAllAsync();*/
            return await _ordenDeCompraRepositorio.GetOrdenesDeCompraProveedor();
        }

        public async Task<OrdenDeCompra> GetByIdOrdenDeCompraAsync(int id)
        {
            return await _ordenDeCompraRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateOrdenDeCompraAsync(OrdenDeCompra ordenDeCompra)
        {
            var ordenCompraExistente = await _ordenDeCompraRepositorio.GetByIdAsync(ordenDeCompra.IdOrden);
            if (ordenCompraExistente == null)
            {
                throw new ArgumentException($"No se encontró una orden de compra con ID {ordenDeCompra.IdOrden}", nameof(ordenDeCompra));
            }

            //Actualizar los campos necesarios
            ordenCompraExistente.NumeroOrden = ordenDeCompra.NumeroOrden;
            ordenCompraExistente.FechaCreacion = ordenDeCompra.FechaCreacion;
            ordenCompraExistente.IdProveedor = ordenDeCompra.IdProveedor;
            ordenCompraExistente.Estado = ordenDeCompra.Estado;
            ordenCompraExistente.FechaEstimadaEntrega = ordenDeCompra.FechaEstimadaEntrega;
            ordenCompraExistente.ArchivoPdf = ordenDeCompra.ArchivoPdf;

            await _ordenDeCompraRepositorio.UpdateAsync(ordenCompraExistente);
        }
    }
}
