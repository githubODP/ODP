using System.ComponentModel.DataAnnotations;

namespace Domain.DueDiligence.Enum
{
    public enum ETipoRessalva
    {
        SELECIONE = 0,
        [Display(Name = "Risco de Incompatibilidade de Horários")] Incompatibilidade = 1,
        [Display(Name = "Servidor Empresário")] Servidor_Empresario = 2,
        [Display(Name = "Existência de Ação Judicial")] AcaoJudicial = 3,
        [Display(Name = "Impeditivos Legais para a Nomeação(Tornar sem Efeito")] Impeditivos_Legais = 4,
        [Display(Name = "Servidor Ativo PR")] Servidor_Ativo = 5,
        [Display(Name = "Servidor Ativo/Outras Esferas")] Servidor_Ativo_Outras = 6,
        [Display(Name = "Existência de Nepotismo")] Nepotismo = 7
    }
}
