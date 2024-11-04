using Domain.Compras.Entidades;


namespace Domain.ConsultaCNPJCPF.DTO.Compras
{
    public class BuscaComprasDTO
    {

        public List<Contrato> Contrato { get; set; }
        public List<Dispensa> Dispensa { get; set; }
        public List<Licitacao> Licitacao { get; set; }
    }
}
