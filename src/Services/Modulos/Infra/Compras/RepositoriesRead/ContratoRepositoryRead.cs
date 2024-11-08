﻿using CGEODP.Core.DomainObjects;
using Domain.Compras.Entidades;
using Domain.Compras.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Compras.RepositoriesRead
{
    public class ContratoRepositoryRead : RepositoryRead<Contrato>, IContratoRepositoryRead, IAggregateRoot
    {
        public ContratoRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<Contrato>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlDecode(cnpj);
            Console.WriteLine($"Buscando contratos com CNPJ: {cnpj}"); // Log para verificar o valor
            return await _context.Set<Contrato>()
            .AsNoTracking()
            .Where(c => c.CNPJCPF == cnpj)
            .Select(c => new Contrato
            {

                OrgaoGestor = c.OrgaoGestor,
                StatusContrato = c.StatusContrato,
                Protocolo = c.Protocolo,
                Fornecedor = c.Fornecedor,
                CNPJCPF = c.CNPJCPF,
                VlrTotalAtual = c.VlrTotalAtual,
                VlrTotalOriginal = c.VlrTotalOriginal,               
                DTInicioVigencia = c.DTInicioVigencia,
                DTFimVigencia = c.DTFimVigencia,
                // Outros campos que você realmente precisa
            })
            .ToListAsync();
        }

        public async Task<List<Contrato>> BuscarCPF(string cpf)
        {
            return await _context.Set<Contrato>()
            .AsNoTracking()
            .Where(c => c.CNPJCPF == cpf)
            .Select(c => new Contrato
            {

                OrgaoGestor = c.OrgaoGestor,
                StatusContrato = c.StatusContrato,
                Protocolo = c.Protocolo,
                VlrTotalAtual = c.VlrTotalAtual,
                VlrTotalOriginal = c.VlrTotalOriginal,
                CNPJCPF = c.CNPJCPF,
                DTInicioVigencia = c.DTInicioVigencia,
                DTFimVigencia = c.DTFimVigencia,
                // Outros campos que você realmente precisa
            })
            .ToListAsync();
        }


    }



}

