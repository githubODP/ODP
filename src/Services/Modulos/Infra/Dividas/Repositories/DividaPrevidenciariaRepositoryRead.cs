﻿using Dividas.Domain.Entidades;
using Dividas.Domain.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Dividas.Infra.Repositories
{
    public class DividaPrevidenciariaRepositoryRead : RepositoryRead<DividaPrevidenciaria>, IDividaPrevidenciaRepositoryRead
    {
        public DividaPrevidenciariaRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<DividaPrevidenciaria>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<DividaPrevidenciaria>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cnpj)
                 .Select(c => new DividaPrevidenciaria
                 {
                     TipoPessoa = c.TipoPessoa,
                     NomeDevedor = c.NomeDevedor,
                     CNPJCPF = c.CNPJCPF,
                     DataInscricao = c.DataInscricao,
                     ValorConsolidado = c.ValorConsolidado,
                 })
                .ToListAsync();

        }

        public async Task<List<DividaPrevidenciaria>> BuscarCPF(string cpf)
        {

            return await _context.Set<DividaPrevidenciaria>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cpf)
                 .Select(c => new DividaPrevidenciaria
                 {
                     TipoPessoa = c.TipoPessoa,
                     NomeDevedor = c.NomeDevedor,
                     CNPJCPF = c.CNPJCPF,
                     DataInscricao = c.DataInscricao,
                     ValorConsolidado = c.ValorConsolidado,
                 })
                .ToListAsync();

        }


    }
}