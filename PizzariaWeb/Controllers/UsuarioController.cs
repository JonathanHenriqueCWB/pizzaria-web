using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.DAL;

namespace PizzariaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        #region Confi
        private readonly UsuarioDAO _usuarioDAO;
        private readonly UserManager<UsuarioLogado> _userManager;
        private readonly SignInManager<UsuarioLogado> _signInManager;
        public UsuarioController(
            UsuarioDAO usuarioDAO, 
            UserManager<UsuarioLogado> userManager,
            SignInManager<UsuarioLogado> signInManager
        )
        {
            _usuarioDAO = usuarioDAO;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
        #region INDEX LISTAR
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region CADASTRAR
        public IActionResult Cadastrar()
        {
            Usuario u = new Usuario();
            if (TempData["Endereco"] != null)
            {
                string resultado = TempData["Endereco"].ToString();
                Endereco endereco = JsonConvert.DeserializeObject<Endereco>(resultado);
                u.Endereco = endereco;
            }
            return View(u);
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar(Usuario usuario)
        {
            UsuarioLogado usuarioLogado = new UsuarioLogado
            {
                UserName = "usuariopreto",
                Email = usuario.Email
            };
            IdentityResult result = await _userManager.CreateAsync(usuarioLogado, usuario.Senha);

            if (result.Succeeded)
            {
                //Logando usuário
                await _signInManager.SignInAsync(usuarioLogado, isPersistent: false);

                if (_usuarioDAO.Cadastrar(usuario))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Este e-mail já está sendo utilizado!");
            }

            //Erros Identity    
            AdicionarErros(result);

            return View(usuario);
        }

        public void AdicionarErros(IdentityResult result)
        {
            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        #endregion
        #region BUSCAR CEP
        [HttpPost]
        public IActionResult BuscarCep(Usuario u)
        {
            string url = "https://viacep.com.br/ws/" + u.Endereco.Cep + "/json/";
            WebClient client = new WebClient();
            TempData["Endereco"] = client.DownloadString(url);
            return RedirectToAction(nameof(Cadastrar));
        }
        #endregion
    }
}