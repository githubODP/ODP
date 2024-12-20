﻿using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace RH.API.Controllers
{
    [ApiController]
    [Route("api/recursoshumanos")]
    [Authorize]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepositoryRead _funcionarioRepositoryRead;



        public FuncionarioController(IFuncionarioRepositoryRead funcionariorepositoryread)
        {
            _funcionarioRepositoryRead = funcionariorepositoryread;

        }



        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _funcionarioRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("buscaId/{id}")]
        public async Task<Funcionario> BuscaId(Guid id)
        {
            return await _funcionarioRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("CPF do funcionário é inválido.");
            }

            var funcionario = await _funcionarioRepositoryRead.BuscarPorCPF(cpf);

            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            return Ok(funcionario);
        }



    }
}
