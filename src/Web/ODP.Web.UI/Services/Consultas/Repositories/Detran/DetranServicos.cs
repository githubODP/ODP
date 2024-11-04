namespace ODP.Web.UI.Services.Consultas.Repositories.Detran
{
    //public class DetranServicos : IDetranServicos
    //{

    //    private readonly IVeiculoBaixadoService _veiculobaixadoService;
    //    private readonly IVeiculoBloqueadoService _veiculoBloqueadoService;
    //    private readonly IVeiculoCirculacaoService _veiculoCirculacaoService;
    //    private readonly IVeiculoIndisponivelService _veiculoIndisponivelService;
    //    private readonly IVeiculoOrdemJudicialService _veiculoOrdemService;
    //    private readonly IVeiculoProntuarioService _veiculoProntuarioService;
    //    private readonly IVeiculoRegistroForaService _veiculoRegistroForaService;


    //    public DetranServicos(IVeiculoBaixadoService veiculobaixadoService,
    //                         IVeiculoBloqueadoService veiculoBloqueadoService,
    //                         IVeiculoCirculacaoService veiculoCirculacaoService,
    //                         IVeiculoIndisponivelService veiculoIndisponivelService,
    //                         IVeiculoOrdemJudicialService veiculoOrdemService,
    //                         IVeiculoProntuarioService veiculoProntuarioService,
    //                         IVeiculoRegistroForaService veiculoRegistroForaService)
    //    {
    //        _veiculobaixadoService = veiculobaixadoService;
    //        _veiculoBloqueadoService = veiculoBloqueadoService;
    //        _veiculoCirculacaoService = veiculoCirculacaoService;
    //        _veiculoIndisponivelService = veiculoIndisponivelService;
    //        _veiculoOrdemService = veiculoOrdemService;
    //        _veiculoProntuarioService = veiculoProntuarioService;
    //        _veiculoRegistroForaService = veiculoRegistroForaService;
    //    }

    //    public async Task<BuscaDetranDTO> BuscarCNPJ(string cnpj)
    //    {
    //        var veiculobaixado = await _veiculobaixadoService.BuscarPorCNPJ(cnpj);
    //        var veiculobloqueado = await _veiculoBloqueadoService.BuscarPorCNPJ(cnpj);
    //        var veiculocirculacao = await _veiculoCirculacaoService.BuscarPorCNPJ(cnpj);
    //        var veiculoindisponivel = await _veiculoIndisponivelService.BuscarPorCNPJ(cnpj);
    //        var veiculoordemjudicial = await _veiculoOrdemService.BuscarPorCNPJ(cnpj);
    //        var veiculoprontuario = await _veiculoProntuarioService.BuscarPorCNPJ(cnpj);
    //        var veiculofora = await _veiculoRegistroForaService.BuscarPorCNPJ(cnpj);

    //        return new BuscaDetranDTO
    //        {
    //            VeiculoBaixado = veiculobaixado,
    //            VeiculoBloqueioRoubo = veiculobloqueado,
    //            VeiculoCirculacao = veiculocirculacao,
    //            VeiculoIndispAdmin = veiculoindisponivel,
    //            VeiculoOrdemJudicial = veiculoordemjudicial,
    //            VeiculoProntuario = veiculoprontuario,
    //            VeiculoRegistroFora = veiculofora,
    //        };
    //    }



    //    public async Task<BuscaDetranDTO> BuscarCPF(string cpf)
    //    {
    //        var veiculobaixado = await _veiculobaixadoService.BuscarPorCPF(cpf);
    //        var veiculobloqueado = await _veiculoBloqueadoService.BuscarPorCPF(cpf);
    //        var veiculocirculacao = await _veiculoCirculacaoService.BuscarPorCPF(cpf);
    //        var veiculoindisponivel = await _veiculoIndisponivelService.BuscarPorCPF(cpf);
    //        var veiculoordemjudicial = await _veiculoOrdemService.BuscarPorCPF(cpf);
    //        var veiculoprontuario = await _veiculoProntuarioService.BuscarPorCPF(cpf);
    //        var veiculofora = await _veiculoRegistroForaService.BuscarPorCPF(cpf);

    //        return new BuscaDetranDTO
    //        {
    //            VeiculoBaixado = veiculobaixado,
    //            VeiculoBloqueioRoubo = veiculobloqueado,
    //            VeiculoCirculacao = veiculocirculacao,
    //            VeiculoIndispAdmin = veiculoindisponivel,
    //            VeiculoOrdemJudicial = veiculoordemjudicial,
    //            VeiculoProntuario = veiculoprontuario,
    //            VeiculoRegistroFora = veiculofora,
    //        };
    //    }
    //}
}
