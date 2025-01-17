using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DueDiligence.Entidade
{
    public class Comissionado : Entity, IAggregateRoot
    {
        [Display(Name = "Nº do Protocolo")]
        [Column("NroProtocolo")]
        public string? NroProtocolo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string? Orgao { get; set; }
        public string? Responsavel { get; set; }
        public string? Indicacao { get; set; }

        [Display(Name = "Data de Solicitacao")]
        [DataType(DataType.Date)]
        public DateTime DataSolicitacao { get; set; }

        [Display(Name = "Data de Resposta")]
        [DataType(DataType.Date)]
        public DateTime DataResposta { get; set; }

        [Display(Name = "Cadastro Auxílio Emergencial e/ou Bolsa-Familia")]
        [Column("CadAuxilio")]
        public bool CadAuxilio { get; set; }

        [Display(Name = "Pessoa Publicamente Exposta  - PPE")]
        [Column("CadPPE")]
        public bool CadPPE { get; set; }


        [Display(Name = "Cadastro como Pessoa Pública e Notória - PPN")]
        [Column("CadPPN")]
        public bool CadPPN { get; set; }

        [Display(Name = "Vínculo até o 3º Nivel com PEP")]
        [Column("CadVinculoDireto")]
        public bool CadVinculoDireto { get; set; }

        [Display(Name = "Doação Eleitoral a Partido")]
        [Column("CadDoacaoEleitoralPartido")]
        public bool CadDoacaoEleitoralPartido { get; set; }

        [Display(Name = "Doação Eleitoral a Candidato")]
        [Column("CadDoacaoEleitoralCandidato")]
        public bool CadDoacaoEleitoralCandidato { get; set; }

        [Display(Name = "Fornecedor a Candidato")]
        [Column("CadFornecedorCandidato")]
        public bool CadFornecedorCandidato { get; set; }


        [Display(Name = "Filiacao Partidaria")]
        [Column("CadFiliacaoPartido")]
        public bool CadFiliacaoPartido { get; set; }

        [Display(Name = "Cadastro no Quadro Geral de Inabilitados")]
        [Column("CadQuadroInabilitados")]
        public bool CadQuadroInabilitados { get; set; }

        [Display(Name = "OFAC - Agência de Controle de Ativos Estrangeiross do EUA")]
        [Column("CadOFAC")]
        public bool CadOFAC { get; set; }

        [Display(Name = "Cadastro na INTERPOL")]
        [Column("CadInterpol")]
        public bool CadInterpol { get; set; }

        [Display(Name = "Cadastro na CVM")]
        [Column("CadCVM")]
        public bool CadCVM { get; set; }

        public int? ClassificacaoRisco { get; set; }
        public string? Observacao { get; set; }
        public string? Evidencias { get; set; }

        [Display(Name = "Nº Decreto")]
        [Column("Decreto")]
        public string? Decreto { get; set; }


        [Display(Name = "Cadastro Nacional de Condenações Cívis por Atos de Improbidade Administrativa")]
        [Column("CadImprobidade")]
        public bool CadImprobidade { get; set; }

        [Display(Name = "Cadastro de Débito PGFN/DAU(S)")]
        [Column("CadDebitoPGFN")]
        public bool CadDebitoPGFN { get; set; }

        [Display(Name = "Vínculo de Parentesco ate 3º Grau com Sócio")]
        [Column("CadParentescoGrauSocio")]
        public bool CadParentescoGrauSocio { get; set; }

        //Impressão pdf

        [Display(Name = "Funcionário Público")]
        [Column("CadFuncionarioPublico")]
        public bool CadFuncionarioPublico { get; set; }


        [Display(Name = "Vínculo de Parentesco ate 3º com Servidor Público Estadual")]
        [Column("CadParentescoGrauServidorPublico")]
        public bool CadParentescoGrauServidorPublico { get; set; }


        [Display(Name = "Aposentado")]
        [Column("CadAposentado")]
        public bool CadAposentado { get; set; }

        [Display(Name = "Pessoa Exposta Politicamente - PEP")]
        [Column("CadPEP")]
        public bool CadPEP { get; set; }

        [Display(Name = "Vínculo até o 3º Nivel com PEP")]
        [Column("CadVinculoPEP")]
        public bool CadVinculoPEP { get; set; }

        [Display(Name = "Micro Empresário Individual/Empresário Individual")]
        [Column("CadMEI")]
        public bool CadMEI { get; set; }

        [Display(Name = "Empresário")]
        [Column("CadEmpresario")]
        public bool CadEmpresario { get; set; }

        [Display(Name = "Empresas Inidôneas e Suspensas - CEIS")]
        [Column("CadEmpresaInidoneos")]
        public bool CadEmpresaInidoneos { get; set; }

        [Display(Name = "Cadastro Nacional de Empresas Punidas - CNEP")]
        [Column("CadCnep")]
        public bool CadCnep { get; set; }

        [Display(Name = "Cadastro de Entidades Privadas Sem Fins Lucrativos Impedidas - CEPIM")]
        [Column("CadCepim")]
        public bool CadCepim { get; set; }


        [Display(Name = "Acordos de Leniência")]
        [Column("CadAcordo")]
        public bool CadAcordo { get; set; }


        [Display(Name = "Cadastro de Doação Eleitoral")]
        [Column("CadDoacaoEleitoral")]
        public bool CadDoacaoEleitoral { get; set; }

        [Display(Name = "Fornecedor a Partido")]
        [Column("CadFornecedorPartido")]
        public bool CadFornecedorPartido { get; set; }

        [Display(Name = "Contas Irregulares TCU")]
        [Column("CadIrregularTCU")]
        public bool CadIrregularTCU { get; set; }


        [Display(Name = "Contas Eleitorais Irregulares TCU")]
        [Column("CadIrregularEleitoralTCU")]
        public bool CadIrregularEleitoralTCU { get; set; }

        [Display(Name = "Cadastro - CNIA")]
        [Column("CadInelegivel")]
        public bool CadInelegivel { get; set; }


        [Display(Name = "Cadastro - CEAF")]
        [Column("CadExpulsoFederal")]
        public bool CadExpulsoFederal { get; set; }

        [Display(Name = "Mandado de Prisão - (CNJ)")]
        [Column("CadPrisao")]
        public bool CadPrisao { get; set; }

        [Display(Name = "Possui  Empresa com Contrato com a Administração Pública")]
        [Column("CadEmpresaContrato")]
        public bool CadEmpresaContrato { get; set; }

        [Display(Name = "Restriçoes TCE/PR")]
        [Column("CadRestricoesTCE")]
        public bool CadRestricoesTCE { get; set; }

        [Display(Name = "Inadimplentes TCE/PR")]
        [Column("CadInadimplentesTCE")]
        public bool CadInadimplentesTCE { get; set; }


        [Display(Name = "Irregularidades TCE/PR")]
        [Column("CadIrregularidadesTCE")]
        public bool CadIrregularidadesTCE { get; set; }

        [Display(Name = "Inabilitados - TCU")]
        [Column("CadInabilitado")]
        public bool CadInabilitado { get; set; }


        [Display(Name = "Inidôneo - TCU")]
        [Column("CadInidoneo")]
        public bool CadInidoneo { get; set; }

        [Display(Name = "Trabalho Escravo")]
        [Column("CadTrabalhoEscravo")]
        public bool CadTrabalhoEscravo { get; set; }
    }

}

