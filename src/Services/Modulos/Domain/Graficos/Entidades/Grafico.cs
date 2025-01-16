using CGEODP.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Graficos.Entidades
{
    public class Grafico : Entity, IAggregateRoot
    {
        public String Content { get; set; }
    }
}
