using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentity.Extensions;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }


        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimGravar()
        {
            return View("Secret");
        }

        [ClaimsAuthorize("Produtos","Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();
            //Tratamento de erros para o usuário
            switch (id)
            {
                case 500:
                    modelErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contato nosso suporte.";
                    modelErro.Title = "Ocorreu um erro";
                    modelErro.ErrorCode = id;
                    break;
                case 404:
                    modelErro.Message = "A página que você está procurando não existe";
                    modelErro.Title = "Ops! Página não encontrada";
                    modelErro.ErrorCode = id;
                    break;
                case 403:
                    modelErro.Message = "Você não tem permissão para executar estar tarefa";
                    modelErro.Title = "Acesso negado";
                    modelErro.ErrorCode = id;
                    break;
                default:
                    return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
