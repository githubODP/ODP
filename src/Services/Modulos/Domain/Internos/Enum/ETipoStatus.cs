using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
