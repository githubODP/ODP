using Domain.GovernoEstadual.Entidades;


namespace Domain.ConsultaCNPJCPF.DTO.GovernoEstadual
{
    public class BuscaEstadualDTO
    {

        public List<PADV> PADV { get; set; }
        public List<Ambiental> Ambiental { get; set; }
    }
}
