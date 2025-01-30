namespace Domain.DueDiligence.Entidade
{
    public class AnaliseResponseDTO
    {
        public Guid Id { get; set; }
        public string NroProtocolo { get; set; }
        public DateTime DataAnalise { get; set; }
        public string AnaliseTecnica { get; set; }
        public string Risco { get; set; }
        public string Ressalvas { get; set; }
        public string Responsavel { get; set; }

        // Dados adicionais do protocolo
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Orgao { get; set; }
        public string Observacao { get; set; }
    }
}

