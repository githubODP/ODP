using System.ComponentModel.DataAnnotations;

namespace Domain.Corregedoria.Enum
{
    public enum ETipoDecisao
    {
        SELECIONE = 0,
        ARQUIVAMENTO = 1,
        [Display(Name = "ADVERTÊNCIA")] ADVERTENCIA = 2,
        [Display(Name = "REPREENSÃO")] REPREENSAO = 3,
        [Display(Name = "SUSPENSÃO")] SUSPENSAO = 4,
        MULTA = 5,
        [Display(Name = "DESTITUIÇÃO DE FUNÇÃO")] DESTITUICAO_FUNCAO = 6,
        [Display(Name = "DEMISSÃO")] DEMISSAO = 7,
        [Display(Name = "ABSOLVIÇÃO")] ABSOLVICAO = 8,
        [Display(Name = "RESCISÃO")] RESCISAO = 9,
        [Display(Name = "ANULADO/REVOGADO")] ANULADO_REVOGADO = 10,
        [Display(Name = "SOBRESTADO/SUSPENSO")] SOBRESTADO_SUSPENSO = 11,
        EXONERADO = 12,
        [Display(Name = "CASSAÇÃO DE APOSENTADORIA OU DISPONIBILIDADE")] CASSACAO_APOSENTADORIA = 13,
        [Display(Name = "EM ANDAMENTO")] EM_ANDAMENTO = 14,
        [Display(Name = "INSTAURAÇÃO DE PAD")] INSTAURACAO_DE_PAD = 15,
        [Display(Name = "NÃO RESPONSABILIZAÇÃO")] NAO_RESPONSABILIZACAO = 16,
        [Display(Name = "PRORROGAÇÃO")] PRORROGACAO = 17,
        PRESCRITO = 18,
        [Display(Name = "TAC CELEBRADO")] TAC_CELEBRADO = 19,
        [Display(Name = "TAC CONCLUÍDO")] TAC_CONCLUIDO = 20,
        [Display(Name = "TAC DESCUMPRIMENTO")] TAC_DESCUMPRIDO = 21,
        [Display(Name = "TAC INVIÁVEL")] TAC_INVIAVEL = 22,
        [Display(Name = "PERDA DE OBJETO")] PERDA_OBJETO = 23

    }

}
