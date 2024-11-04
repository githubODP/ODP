using System;

namespace ODP.Web.UI.Models.ViewModels.Tribunal.TSE
{
    public class DoacaoCandidatoViewModel
    {
        public Guid Id { get; set; }
        public string UF { get; set; }
        public string Municipio { get; set; }
        public int IDPrestacaoContas { get; set; }
        public string CNPJPrestadorConta { get; set; }
        public string NomeCandidato { get; set; }
        public string CPFCandidato { get; set; }
        public string Cargo { get; set; }
        public string SiglaPartido { get; set; }
        public string NomePartido { get; set; }
        public string DescricaoOrigemReceita { get; set; }
        public string DescricaoEspecieReceita { get; set; }
        public string NomeDoador { get; set; }
        public string CPFDoador { get; set; }
        public string NroReciboDoacao { get; set; }
        public string NroDoctoDoacao { get; set; }
        public DateTime DataReceita { get; set; }
        public string DescricaoReceita { get; set; }
        public float ValorDoacao { get; set; }
        public int AnoEleicao { get; set; }
    }
}
