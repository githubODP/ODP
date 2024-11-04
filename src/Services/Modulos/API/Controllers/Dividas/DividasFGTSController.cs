
using Dividas.Domain.Entidades;
using Dividas.Domain.Interfaces;
using Domain.Compras.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dividas.API.Controllers
{
    [ApiController]
    [Route("api/dividafgts")]
    [Authorize]
    public class DividasFGTSController : Controller
    {

        private readonly IDividaFgtsRepositoryRead _fgtsRepositoryRead;



        public DividasFGTSController(IDividaFgtsRepositoryRead fgtsRepositoryRead)
        {
            _fgtsRepositoryRead = fgtsRepositoryRead;


        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _fgtsRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consulta/{id}")]
        public async Task<DividaFGTS> BuscaId(Guid id)
        {
            return await _fgtsRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var dividas = await _fgtsRepositoryRead.BuscarCNPJ(cnpj);

            if (dividas == null || !dividas.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CNPJ fornecido.");
            }

            return Ok(dividas);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var dividas = await _fgtsRepositoryRead.BuscarCPF(cpf);

            if (dividas == null || !dividas.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CPF fornecido.");
            }

            return Ok(dividas);
        }


    }
}
