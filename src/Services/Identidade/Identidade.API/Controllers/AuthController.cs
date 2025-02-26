using CGEODP.WebApi.Core.Identidade;
using Identidade.Domain.Entidades;
using Identidade.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identidade.API.Controllers
{
    [Route("api/identidade")]


    public class AuthController : MainController
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ILoggedInUsersService _loggedInUsersService;


        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              ILoggedInUsersService loggedInUsersService)
        {


            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _loggedInUsersService = loggedInUsersService;
        }

        [HttpGet("usuarios-logados")]
        public ActionResult<List<string>> ListarUsuariosLogados()
        {
            var loggedInUsers = _loggedInUsersService.GetLoggedInUsers();
            return Ok(loggedInUsers);
        }




        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                // Adiciona a role padrão
                var roleResult = await _userManager.AddToRoleAsync(user, usuarioRegistro.Role);
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        AdicionarErroProcessamento(error.Description);
                    }
                }

                // Adiciona o claim do departamento convertendo o enum para string
                var claimDepartamento = new Claim("Departamento", usuarioRegistro.Departamento.ToString());
                var claimResult = await _userManager.AddClaimAsync(user, claimDepartamento);
                if (!claimResult.Succeeded)
                {
                    foreach (var error in claimResult.Errors)
                    {
                        AdicionarErroProcessamento(error.Description);
                    }
                }

                return CustomResponse(await GerarJwt(usuarioRegistro.Email));
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            return CustomResponse();
        }



        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(usuarioLogin.Email);
                _loggedInUsersService.AddUser(user.Email);

                return CustomResponse(await GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou Senha incorretos");
            return CustomResponse();
        }



        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            // Se desejar, verifique se o claim "Department" existe e adicione se necessário
            if (!claims.Any(c => c.Type == "Department"))
            {
                // Aqui você pode definir um valor padrão ou obter essa informação de outro lugar
                claims.Add(new Claim("Department", "DepartamentoPadrão"));
            }

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }


        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));

            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioToken>>> ListarUsuarios()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            var usuariosComRoles = new List<UsuarioToken>();

            foreach (var usuario in usuarios)
            {
                var claims = await _userManager.GetClaimsAsync(usuario);
                var roles = await _userManager.GetRolesAsync(usuario);

                var usuarioToken = new UsuarioToken
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value }),
                    Roles = roles
                };

                usuariosComRoles.Add(usuarioToken);
            }

            return Ok(usuariosComRoles);
        }



        [HttpPut("desabilitar-usuario/{userId}")]
        public async Task<ActionResult> DesabilitarUsuario(string userId)
        {
            var usuario = await _userManager.FindByIdAsync(userId);

            if (usuario == null)
            {
                AdicionarErroProcessamento("Usuário não encontrado");
                return CustomResponse();
            }

            var result = await _userManager.SetLockoutEndDateAsync(usuario, DateTimeOffset.UtcNow.AddYears(100));

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    AdicionarErroProcessamento(error.Description);
                }
                return CustomResponse();
            }

            return CustomResponse();
        }




        [HttpPut("habilitar-usuario/{userId}")]
        public async Task<ActionResult> HabilitarUsuario(string userId)
        {
            var usuario = await _userManager.FindByIdAsync(userId);

            if (usuario == null)
            {
                AdicionarErroProcessamento("Usuário não encontrado");
                return CustomResponse();
            }

            var result = await _userManager.SetLockoutEndDateAsync(usuario, null);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    AdicionarErroProcessamento(error.Description);
                }
                return CustomResponse();
            }

            return CustomResponse();
        }


        [HttpPut("redefinir-senha")]
        public async Task<ActionResult> RedefinirSenha(RedefinirSenha model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NovaSenha);

            if (result.Succeeded)
            {
                return Ok("Senha redefinida com sucesso");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }


    }
}
