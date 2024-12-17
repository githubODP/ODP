using CGEODP.Core.DomainObjects;


namespace Domain.GovernoFederal.Entidades
{
    public class Aeronave : Entity, IAggregateRoot
    {


        public string Marca { get; set; }
        public string Proprietario { get; set; }
        public string OutrosProprietarios { get; set; }
        public string SGUF { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string Operador { get; set; }
        public string OutrosOperadores { get; set; }
        public string UFOperador { get; set; }
        public string CPFCGC { get; set; }
        public string Matricula { get; set; }
        public string NroSerie { get; set; }
        public string Categoria { get; set; }
        public string CodTipo { get; set; }
        public string Modelo { get; set; }
        public string Fabricante { get; set; }
        public string CDInterdicao { get; set; }
        public string MarcaNacional { get; set; }
        public string DescricaoGravame { get; set; }


    }
}
