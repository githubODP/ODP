using CGEODP.Core.Data;
using Domain.GovernoFederal.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GovernoFederal.Interfaces
{
    public  interface ICnepRepositoryRead : IRepositoryRead<Cnep>, IBuscaInfo<Cnep>
    {
    }
}
