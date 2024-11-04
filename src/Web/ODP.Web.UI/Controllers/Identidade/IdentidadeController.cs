using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Identidade;
using ODP.Web.UI.Services.Identidade;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Identidade
{

    public class IdentidadeController : MainController
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet]


        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]


        public async Task<IActionResult> Registro(UsuarioRegistroViewModel usuarioRegistro)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioRegistro);
            }

            var resposta = await _autenticacaoService.Registro(usuarioRegistro);

            if (ResponsePossuiErros(resposta.ResponseResult))
            {

                return View(usuarioRegistro);
            }

            await RealizarLogin(resposta);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]


        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]


        public async Task<IActionResult> Login(UsuarioLoginViewModel usuarioLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(usuarioLogin);

            var resposta = await _autenticacaoService.Login(usuarioLogin);

            if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioLogin);

            await RealizarLogin(resposta);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }




        [HttpGet]


        public async Task<IActionResult> Logout()
        {
            // Obtém o e-mail do usuário logado
            var email = User.Identity.Name;

            // Realiza o logout do usuário e remove o cookie de autenticação
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Limpa o contexto do usuário
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // Atualiza a lista de usuários logados após o logout
            await _autenticacaoService.ListarUsuariosLogados(); // Se necessário para atualizar a lista no backend

            // Redireciona para a página inicial
            return RedirectToAction("Index", "Home");
        }

        private async Task RealizarLogin(UsuarioRespostaLoginViewModel resposta)
        {


            var token = ObterTokenFormatado(resposta.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", resposta.AccessToken));
            claims.AddRange(token.Claims);


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }


        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _autenticacaoService.ListarUsuarios();
            return View(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> DesabilitarUsuario(string userId)
        {
            var result = await _autenticacaoService.DesabilitarUsuario(userId);
            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "Falha ao desabilitar o usuário" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> HabilitarUsuario(string userId)
        {
            var result = await _autenticacaoService.HabilitarUsuario(userId);
            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "Falha ao habilitar o usuário" });
            }
        }



        [HttpGet]

        public IActionResult RedefinirSenha(string email = null)
        {
            var model = new RedefinirSenhaViewModel
            {
                Email = email
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(RedefinirSenhaViewModel redefinirSenhaModel)
        {
            if (!ModelState.IsValid)
            {
                return View(redefinirSenhaModel);
            }

            var result = await _autenticacaoService.RedefinirSenha(redefinirSenhaModel);

            if (!result.Sucesso)
            {
                foreach (var error in result.Erros)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(redefinirSenhaModel);
            }

            TempData["Sucesso"] = "Senha redefinida com sucesso!";
            return RedirectToAction("ListarUsuarios");
        }


        [HttpGet]

        public async Task<IActionResult> ListarUsuariosLogados()
        {
            var usuariosLogados = await _autenticacaoService.ListarUsuariosLogados();
            return View(usuariosLogados);
        }




    }

}
