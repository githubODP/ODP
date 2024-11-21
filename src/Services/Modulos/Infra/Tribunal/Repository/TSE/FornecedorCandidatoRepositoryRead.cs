using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Tribunal.Repository.TSE
{
    public class FornecedorCandidatoRepositoryRead : RepositoryRead<FornecedorCandidato>, IFornecedorCandidatoRepositoryRead
    {
        public FornecedorCandidatoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<FornecedorCandidato>> BuscarCNPJ(string cnpj)
        {
            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<FornecedorCandidato>()
                                 .AsNoTracking()
                                 .Where(c => c.CNPJCPF == cnpj)
            .Select(c => new FornecedorCandidato
            {
                AnoEleicao = c.AnoEleicao,
                Municipio = c.Municipio,
                Candidato = c.Candidato,
                CPFCandidato = c.CPFCandidato,
                DescricaoCargo = c.DescricaoCargo,
                Partido = c.Partido,
                CNPJCPF = c.CNPJCPF,
                Fornecedor = c.Fornecedor,
                ValorDespesas = c.ValorDespesas,

            })
            .ToListAsync();
        }

        public async Task<List<FornecedorCandidato>> BuscarCPF(string cpf)
        {
            return await _context.Set<FornecedorCandidato>()
                                  .AsNoTracking()
                                  .Where(c => c.CNPJCPF == cpf)
                                .Select(c => new FornecedorCandidato
                                {
                                    AnoEleicao = c.AnoEleicao,
                                    Municipio = c.Municipio,
                                    Candidato = c.Candidato,
                                    CPFCandidato = c.CPFCandidato,
                                    DescricaoCargo = c.DescricaoCargo,
                                    Partido = c.Partido,
                                    CNPJCPF = c.CNPJCPF,
                                    Fornecedor = c.Fornecedor,
                                    ValorDespesas = c.ValorDespesas,

                                })
            .ToListAsync();
        }
    }
}
