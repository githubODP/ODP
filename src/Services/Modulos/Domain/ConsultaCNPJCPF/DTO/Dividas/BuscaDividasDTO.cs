using Dividas.Domain.Entidades;


namespace Domain.ConsultaCNPJCPF.DTO.Dividas
{
    public class BuscaDividasDTO
    {
        public List<DividaFGTS> DividaFGTS { get; set; }
        public List<DividaNaoPrevidenciaria> DividaNaoPrevidenciaria { get; set; }
        public List<DividaPrevidenciaria> DividaPrevidenciaria { get; set; }
    }
}
