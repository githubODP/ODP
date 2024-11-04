using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models;
using ODP.Web.UI.Models.Identidade;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Identidade
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);

            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLoginViewModel> Login(UsuarioLoginViewModel usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLoginViewModel
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLoginViewModel>(response);
        }

        public async Task<UsuarioRespostaLoginViewModel> Registro(UsuarioRegistroViewModel usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLoginViewModel
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLoginViewModel>(response);
        }


        public async Task<IEnumerable<UsuarioTokenViewModel>> ListarUsuarios()
        {
            var response = await _httpClient.GetAsync("/api/identidade/usuarios");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<UsuarioTokenViewModel>>(response);
        }



        public async Task<bool> DesabilitarUsuario(string userId)
        {
            var response = await _httpClient.PutAsync($"/api/identidade/desabilitar-usuario/{userId}", null);

            return TratarErrosResponse(response);
        }

        public async Task<bool> HabilitarUsuario(string userId)
        {
            var response = await _httpClient.PutAsync($"/api/identidade/habilitar-usuario/{userId}", null);

            return TratarErrosResponse(response);
        }


        public async Task<ResultadoRedefinicaoSenhaViewModel> RedefinirSenha(RedefinirSenhaViewModel redefinirSenhaModel)
        {
            var resetSenhaContent = ObterConteudo(redefinirSenhaModel);

            var response = await _httpClient.PutAsync("/api/identidade/redefinir-senha", resetSenhaContent);

            if (!TratarErrosResponse(response))
            {
                // Retorna um objeto de resultado indicando falha com os erros obtidos
                var errors = await DeserializarObjetoResponse<IEnumerable<string>>(response);
                return new ResultadoRedefinicaoSenhaViewModel { Sucesso = false, Erros = errors };
            }

            // Retorna um objeto de resultado indicando sucesso
            return new ResultadoRedefinicaoSenhaViewModel { Sucesso = true };
        }

        public async Task<List<string>> ListarUsuariosLogados()
        {
            var response = await _httpClient.GetAsync("/api/identidade/usuarios-logados");

            if (!TratarErrosResponse(response))
            {
                return new List<string>();
            }

            return await DeserializarObjetoResponse<List<string>>(response);
        }


    }
}
