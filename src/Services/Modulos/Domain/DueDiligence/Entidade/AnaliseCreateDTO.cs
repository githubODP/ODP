using Domain.DueDiligence.Enum;

namespace Domain.DueDiligence.Entidade
{
    public class AnaliseCreateDTO
    {
        public Guid ComissionadoId { get; set; }  // Adicionado para vincular ao comissionado correto
        public string NroProtocolo { get; set; }
        public DateTime DataAnalise { get; set; }
        public string AnaliseTecnica { get; set; }
        public ETipoRisco Risco { get; set; }
        public ETipoRessalva Ressalvas { get; set; }
        public string Responsavel { get; set; }



    }
}
