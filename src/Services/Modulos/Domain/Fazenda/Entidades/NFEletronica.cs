
using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.Fazenda.Entidades
{
    public class NFEletronica : Entity, IAggregateRoot
    {

        public string NroReceitaEstadual { get; set; }
        public int CodigoUF { get; set; }
        public int CodigoNF { get; set; }
        public string NaturezaOperacao { get; set; }
        public int ModeloNF { get; set; }
        public int SerieNF { get; set; }
        public int NumeroNF { get; set; }

        [Display(Name = "Data Emissao")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int TipoNF { get; set; }

        [Display(Name = "CNPJ Emitente")]
        public string CNPJ { get; set; }
        public string Emitente { get; set; }
        public string EnderecoEmitente { get; set; }
        public string NroEmitente { get; set; }
        public string BairroEmitente { get; set; }
        public string MunicipioEmitente { get; set; }
        public string UFEmitente { get; set; }
        public string CEPEmitente { get; set; }
        public string CNPJDestinatario { get; set; }
        public string Destinatario { get; set; }
        public string EnderecoDestinatario { get; set; }
        public string NroDestinatario { get; set; }
        public string BairroDestinatario { get; set; }
        public string MunicipioDestinatario { get; set; }
        public string UFDestinatario { get; set; }
        public string CEPDestinatario { get; set; }
        public string CodigoProduto { get; set; }
        public string Produto { get; set; }
        public string UnidadeProduto { get; set; }
        public float QuantidadeProduto { get; set; }
        public float VlrUnitarioProduto { get; set; }
        public float ValorProduto { get; set; }
        public float ValorNotaFiscal { get; set; }
        public string DetalhamentoProduto { get; set; }
        public string InformacoesAdicionais { get; set; }

    }
}
