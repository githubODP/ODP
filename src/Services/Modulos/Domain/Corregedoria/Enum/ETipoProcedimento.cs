using System.ComponentModel.DataAnnotations;

namespace Domain.Corregedoria.Enum
{
    public enum ETipoProcedimento
    {
        SELECIONE = 0,
        [Display(Name = "SINDICÂNCIA")] SINDICANCIA = 1,
        PAD = 2,
        PAAR = 3,
        PAR = 4,
        PAS = 5,
        TAC = 6,
        PIP = 7,
        TCE = 8,
        [Display(Name = "PROCEDIMENTO MILITAR")] PD_PM_PR = 9,
        [Display(Name = "SINDICÂNCIA/PSS")] SINDICANCIA_PSS = 10,
        CONTROLADORIA = 11,
        [Display(Name = "INSPEÇÕES EM ANDAMENTO")] INSPECOES_ANDAMENTO = 12,
        [Display(Name = "INSPEÇÕES CONCLUIDAS")] INSPECOES_CONCLUIDAS = 13,
        [Display(Name = "VERIFICAÇÃO PRELIMINAR")] VERIFICACAO_PRELIMINAR = 14

    }


}
