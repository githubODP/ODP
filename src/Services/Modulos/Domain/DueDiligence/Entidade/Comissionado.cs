using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DueDiligence.Entidade
{
    public class Comissionado : Entity, IAggregateRoot
    {
        [Display(Name = "Nº do Protocolo")]
        [Column("NroProtocolo")]
        public string NroProtocolo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Orgao { get; set; }

        public string Indicacao { get; set; }

        [Display(Name = "Nº Decreto")]
        [Column("Decreto")]
        public string Decreto { get; set; }


        [Display(Name = "Data de Solicitacao")]
        [DataType(DataType.Date)]
        public DateTime DataSolicitacao { get; set; }


        [Display(Name = "Data de Resposta")]
        [DataType(DataType.Date)]
        public DateTime DataResposta { get; set; }

        public int ClassificacaoRisco { get; set; }
        public string Observacao { get; set; }
        public string Evidencias { get; set; }


        [Display(Name = "Empresário")]
        [Column("CadEmpresario")]
        public bool CadEmpresario { get; set; }

        [Display(Name = "Micro Empreendedor Individual")]
        [Column("CadMEI")]
        public bool CadMEI { get; set; }


        [Display(Name = "Pessoa Publicamente Exposta  - PPE")]
        [Column("CadPPE")]
        public bool CadPPE { get; set; }

        [Display(Name = "Pessoa Exposta Politicamente - PEP")]
        [Column("CadPEP")]
        public bool CadPEP { get; set; }

        [Display(Name = "Vinculo até o 3º Nivel com PEP")]
        [Column("CadVinculoDireto")]
        public bool CadViculoPEP { get; set; }

        [Display(Name = "Doação Eleitoral a Candidato")]
        [Column("CadDoacaoEleitoralCandidato")]
        public bool CadDoacaoEleitoralCandidato { get; set; }

        [Display(Name = "Doação Eleitoral a Partido")]
        [Column("CadDoacaoEleitoralPartido")]
        public bool CadDoacaoEleitoralPartido { get; set; }

        [Display(Name = "Fornecedor a Partido")]
        [Column("CadFornecedorPartido")]
        public bool CadFornecedorPartido { get; set; }

        [Display(Name = "Fornecedor a Candidato")]
        [Column("CadFornecedorCandidato")]
        public bool CadFornecedorCandidato { get; set; }

        [Display(Name = "Filiação Partidária")]
        [Column("CadFiliacaoPartido")]
        public bool CadFiliacaoPartido { get; set; }

        [Display(Name = "Empresas Inidôneas e Suspensas - CEIS")]
        [Column("CadEmpresaInidoneos")]
        public bool CadEmpresaInidoneos { get; set; }

        [Display(Name = "Funcionário Público")]
        [Column("CadFuncionarioPublico")]
        public bool CadFuncionarioPublico { get; set; }

        [Display(Name = "Aposentado")]
        [Column("CadAposentado")]
        public bool CadAposentado { get; set; }


        [Display(Name = "Possui  Empresa com Contrato com a Administração Pública")]
        [Column("CadEmpresaContrato")]
        public bool CadEmpresaContrato { get; set; }

        [Display(Name = "Trabalho Escravo")]
        [Column("CadTrabalhoEscravo")]
        public bool CadTrabalhoEscravo { get; set; }

        [Display(Name = "Mandado de Prisão - (CNJ)")]
        [Column("CadPrisao")]
        public bool CadPrisao { get; set; }

        [Display(Name = "Cadastro - CEAF")]
        [Column("CadExpulsoFederal")]
        public bool CadExpulsoFederal { get; set; }

        [Display(Name = "OFAC - Agência de Controle de Ativos Estrangeiros do EUA")]
        [Column("CadOFAC")]
        public bool CadOFAC { get; set; }

        [Display(Name = "Cadastro - CNIA")]
        [Column("CadInelegivel")]
        public bool CadInelegivel { get; set; }

        [Display(Name = "Inabilitados - TCU")]
        [Column("CadInabilitado")]
        public bool CadInabilitado { get; set; }


        [Display(Name = "Inidôneo - TCU")]
        [Column("CadInidoneo")]
        public bool CadInidoneo { get; set; }

        [Display(Name = "Restriçoes TCE/PR")]
        [Column("CadRestricoesTCE")]
        public bool CadRestricoesTCE { get; set; }

        [Display(Name = "Inadimplentes TCE/PR")]
        [Column("CadInadimplentesTCE")]
        public bool CadInadimplentesTCE { get; set; }


        [Display(Name = "Irregularidades TCE/PR")]
        [Column("CadIrregularidadesTCE")]
        public bool CadIrregularidadesTCE { get; set; }

    }
}
