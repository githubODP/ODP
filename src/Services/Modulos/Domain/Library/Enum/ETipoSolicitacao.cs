using System.ComponentModel.DataAnnotations;

namespace Domain.Library.Enum
{
    public enum ETipoSolicitacao
    {
        SELECIONE = 0,
        [Display(Name = "Ofício")] OFICIO = 1,
        MEMORANDO = 2,
        [Display(Name = "Solicitações")] SOLICITACAO = 3,
        [Display(Name = "Leis Estaduais")] LEISESTADUAIS = 4,
        [Display(Name = "Leis Federais")] LEISFEDERAIS = 5,
        DECRETOS = 6,


    }
}
