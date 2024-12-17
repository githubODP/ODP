using System;

namespace ODP.Web.UI.Models.ViewModels.Fazenda
{
    public class JuceparViewModel
    {
        public Guid Id { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string RazaoSocial { get; set; }
        public string NIRE { get; set; }
        public string NomesSocio { get; set; }
        public string Vinculo { get; set; }
        public string Situacao { get; set; }
        public string MEI { get; set; }
        public DateTime? EntradaSociedade { get; set; }
        public DateTime? SaidaSociedade { get; set; }
        public DateTime? InicioMandato { get; set; }
        public DateTime? TerminoMandato { get; set; }
        public float CapitalSocial { get; set; }
        public DateTime? Constituicao { get; set; }
        public DateTime? InicioAtividade { get; set; }
        public DateTime? TerminoAtividade { get; set; }
        public string Endereco { get; set; }
        public string Nro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string CEP { get; set; }
    }
}
