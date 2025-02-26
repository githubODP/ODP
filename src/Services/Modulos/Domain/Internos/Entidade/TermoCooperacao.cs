

using CGEODP.Core.DomainObjects;
using Domain.Internos.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Internos.Entidade
{
    public class TermoCooperacao : Entity, IAggregateRoot
    {

        public string Protocolo { get; set; }
        public string Orgao { get; set; }
        public string Sigla { get; set; }
        public string NroTermo { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FimVigencia { get; set; }
        public int Validade { get; set; }
        public bool Ativo { get; set; }
        public ETipoStatus Status { get; set; }
        public bool Renovar { get; set; }
        public int DIOE { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Objeto { get; set; }
        public string Regulamentacao { get; set; }
        public string Informacoes { get; set; }
        public string Observacao { get; set; }

        [NotMapped]
        public int DiasRestantes => Math.Max((FimVigencia - DateTime.Today).Days, 0);

    }
}