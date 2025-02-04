using Domain.TermoCooperacao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TermoCooperacao
{
    [ApiController]
    [Route("api/termocooperacao")]
    [Authorize]
    public class TermoCooperacaoController : Controller
    {
        private readonly ITermoCooperacaoRepositoryRead _termoRepositoriyRead;

        public TermoCooperacaoController(ITermoCooperacaoRepositoryRead termoRepositoriyRead)
        {
            _termoRepositoriyRead = termoRepositoriyRead;
        }

        [HttpGet("teste")]

        public string Texto() {
            return "ta funcionando";
        }
    }
}
