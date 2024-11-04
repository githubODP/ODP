using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models;
using System.Linq;

namespace ODP.Web.UI.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors != null && resposta.Errors.Mensagens != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}
