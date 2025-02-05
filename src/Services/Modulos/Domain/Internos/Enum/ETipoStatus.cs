using System.ComponentModel.DataAnnotations;

namespace Domain.Internos.Enum
{
    public enum ETipoStatus
    {

        SELECIONE = 0,
        EXPIRADO = 1,
        [Display(Name = "Assinado Vigente")] ASSINADO_VIGENTE = 2,
        TRAMITANDO = 3,

    }
}
