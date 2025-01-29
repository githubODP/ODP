
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.ResultadoConsulta
{
    public interface IBuscaService
    {
        Task<ResultadoDTO> BuscarCNPJ(string cnpj);
        Task<ResultadoDTO> BuscarCPF(string cpf);
        Task<ResultadoDTO> BuscarCNPJPorTabelasSelecionadas(string cnpj, List<string> tabelasSelecionadas);
        Task<ResultadoDTO> BuscarCPFPorTabelasSelecionadas(string cpf, List<string> tabelasSelecionadas);
        Task<List<ResultadoDTO>> BuscarEmLoteAsync(List<string> identificadores, bool isCNPJ, List<string> tabelasSelecionadas);
        Task<FileStreamResult> GerarPDF(ResultadoDTO dados, string cnpj);




    }





}

