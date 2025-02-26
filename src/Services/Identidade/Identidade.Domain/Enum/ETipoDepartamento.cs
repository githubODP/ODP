using System.ComponentModel.DataAnnotations;

namespace Identidade.Domain.Enum
{
    public enum  ETipoDepartamento
    {
        SELECIONE = 0,
        [Display(Name = "CORREGEDORIA")] CORREGEDORIA = 1,
        [Display(Name = "CONTROLE INTERNO")] CONTROLE_INTERNO = 2,
        [Display(Name = "AUDITORIA")] AUDITORIA = 3,
        [Display(Name = "OBSERVATÓRIO")] ODP= 4,
        [Display(Name = "GABINETE")] GABINETE = 5,
        [Display(Name = "ADMINISTRADOR")] ADMINISTRADOR = 10,

    }
}
