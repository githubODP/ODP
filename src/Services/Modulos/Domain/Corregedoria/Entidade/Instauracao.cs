using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Enum;
using System.ComponentModel.DataAnnotations;


namespace Domain.Corregedoria.Entidade
{
    public class Instauracao : Entity, IAggregateRoot
    {
        public int Ano { get; set; }
        public string? CNPJCPF { get; set; }
        public string? RG { get; set; }
        public ETipoOrgao Orgao { get; set; }
        public ETipoProcedimento Procedimento { get; set; }
        public string? Protocolo { get; set; }
        public string? Objeto { get; set; }
        public string? AtoNormativo { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public int? NumeroDIOE { get; set; }
        public string? AtoNormativoDecisao { get; set; }
        public DateTime? DataPublicacaoDecisao { get; set; }
        public int? NumeroDIOEDecisao { get; set; }
        public ETipoDecisao Decisao { get; set; }
        public string? InfoAdd { get; set; }



        /// Campos validos somente se a opção TAC do Enum ETipoProcedimento for igual a "TAC"


        public string? Obrigacao { get; set; }
        public DateTime? DataInicioTac { get; set; }
        public DateTime? DataFimTac { get; set; }
        public int? PrazoEncerra { get; set; }
        [Display(Name = "PGE")]
        public bool? PGE { get; set; } = false;

        [Display(Name = "Cumpriu")]
        public bool? Cumpriu { get; set; } = false;
        public string? ObservacaoAjusteTAC { get; set; }



        //public List<string> ValidarCamposTac()
        //{
        //    var erros = new List<string>();

        //    if (Procedimento == ETipoProcedimento.TAC)
        //    {
        //        if (DataInicioTac == null)
        //            erros.Add("A 'Data de Início do TAC' é obrigatória para o Procedimento TAC.");
        //        if (DataFimTac == null)
        //            erros.Add("A 'Data de Fim do TAC' é obrigatória para o Procedimento TAC.");
        //        if (PrazoEncerra <= 0)
        //            erros.Add("O 'Prazo para Encerramento' deve ser maior que zero para o Procedimento TAC.");
        //    }

        //    return erros;
        //}



        


    }

}

