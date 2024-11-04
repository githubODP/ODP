using System;

namespace ODP.Web.UI.Models.ViewModels.Detran
{
    public class VeiculoBaixadoViewModel
    {
        public Guid Id { get; set; }
        public string CNPJCPF { get; set; }
        public string Proprietario { get; set; }
        public string Situacao { get; set; }
        public string Tipo { get; set; }
        public string Placa { get; set; }
        public int Renavan { get; set; }
        public string MarcaModelo { get; set; }
        public string Cor { get; set; }
        public int Fabricacao { get; set; }
        public string Modelo { get; set; }
        public DateTime Aquisicao { get; set; }
        public string CNPJCPFAnterior { get; set; }
        public string ProprietarioAnterior { get; set; }
    }
}
