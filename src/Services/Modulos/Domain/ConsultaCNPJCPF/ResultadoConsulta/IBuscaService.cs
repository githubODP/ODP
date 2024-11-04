namespace Domain.ConsultaCNPJCPF
{
    public interface IBuscaService
    {
        IAsyncEnumerable<object> BuscarCNPJ(string cnpj, List<string> tabelasSelecionadas);
        IAsyncEnumerable<object> BuscarCPF(string cpf, List<string> tabelasSelecionadas);

    }
}
