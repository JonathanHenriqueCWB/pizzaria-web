using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using EcommerceEcoville.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.DAL;

namespace PizzariaWeb.Controllers
{
    public class PedidoController : Controller
    {
        #region DAO configuração
        private Venda venda;
        private Pizza pizza;
        private List<ItemBebida> bebidas;
        private List<ItemPizza> pizzas;
        private readonly TamanhoDAO _tamanhoDAO;
        private readonly SaborDAO _saborDAO;
        private readonly BebidaDAO _bebidaDAO;
        private readonly VendaDAO _VendaDAO;
        private readonly UtilsSession _utilsSession;

        private readonly UsuarioDAO _usuarioDAO;
        private readonly UserManager<UsuarioLogado> _userManager;
        private readonly SignInManager<UsuarioLogado> _signInManager;

        public PedidoController(TamanhoDAO tamanhoDAO, SaborDAO saborDAO, BebidaDAO bebidaDAO, VendaDAO vendaDAO, UtilsSession utilsSession,
            UsuarioDAO usuarioDAO, UserManager<UsuarioLogado> userManager, SignInManager<UsuarioLogado> signInManager)
        {
            pizzas = new List<ItemPizza>();

            venda = new Venda();
            pizza = new Pizza();
            _tamanhoDAO = tamanhoDAO;
            _saborDAO = saborDAO;
            _bebidaDAO = bebidaDAO;
            _VendaDAO = vendaDAO;
            _utilsSession = utilsSession;

            _usuarioDAO = usuarioDAO;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        public IActionResult Index()
        {
            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());
            pizzas = JsonConvert.DeserializeObject<List<ItemPizza>>(_utilsSession.RetonarPizzas());
            bebidas = JsonConvert.DeserializeObject<List<ItemBebida>>(_utilsSession.RetonarBebidas());
            
            if (pizza.Tamanho != null)
            {
                ViewBag.Tamanho = pizza.Tamanho.Nome;
            }

            if (pizza.itemSabores.Count != 0)
            {
                foreach (ItemSabor sabor in pizza.itemSabores)
                {
                    ViewBag.Sabor += sabor.Sabor.Nome + ", ";
                }

            }

            if (bebidas.Count != 0)
            {
                foreach (ItemBebida bebida in bebidas)
                {
                    ViewBag.Bebida += bebida.Bebida.Nome + ", ";
                }

            }

            /*if (TempData["Sabor"] != null)
            {
                ViewBag.Sabor = TempData["Sabor"].ToString();
            }
            if (TempData["Bebida"] != null)
            {
                ViewBag.Bebida = TempData["Bebida"].ToString();
            }*/

            ViewBag.ListaTamanho = _tamanhoDAO.Listar();
            ViewBag.ListaSabor = _saborDAO.Listar();
            ViewBag.ListaBebida = _bebidaDAO.Listar();
            return View(venda);
        }

        public IActionResult SelecionarTamanho(int TamanhoId)
        {
            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());

            pizza.Tamanho = _tamanhoDAO.BuscarPorId(TamanhoId);
            _utilsSession.AtualizarPizza(pizza);

            return RedirectToAction("Index");
        }

        public IActionResult SelecionarSabor(int SaborId)
        {
            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());

            ItemSabor itemSabor = new ItemSabor();
            itemSabor.Sabor = _saborDAO.BuscarPorId(SaborId);
            pizza.itemSabores.Add(itemSabor);
            _utilsSession.AtualizarPizza(pizza);

            return RedirectToAction("Index");
        }

        public IActionResult SelecionarBebida(int BebidaId)
        {
            bebidas = JsonConvert.DeserializeObject<List<ItemBebida>>(_utilsSession.RetonarBebidas());

            ItemBebida itemBebida = new ItemBebida();
            itemBebida.Bebida = _bebidaDAO.BuscarPorId(BebidaId);
            bebidas.Add(itemBebida);
            _utilsSession.AtualizarBebida(bebidas);

            return RedirectToAction("Index");
        }

        public IActionResult FecharPizza()
        {
            pizzas = JsonConvert.DeserializeObject<List<ItemPizza>>(_utilsSession.RetonarPizzas());
            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());
            ItemPizza p = new ItemPizza();
            p.Pizza = pizza;

            pizzas.Add(p);

            _utilsSession.AtualizarPizzas(pizzas);
            
            return RedirectToAction("Index");

        }

        public IActionResult TerminarPedido()
        {
            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());
            pizzas = JsonConvert.DeserializeObject<List<ItemPizza>>(_utilsSession.RetonarPizzas());
            bebidas = JsonConvert.DeserializeObject<List<ItemBebida>>(_utilsSession.RetonarBebidas());

            //pizza > listaSabores, tamanho
            //venda > lista de ItemBebida > bebida
            //venda > lista de itemPizza                       

            Usuario usuario = new Usuario();

            if (_signInManager.IsSignedIn(User))
            {
                usuario.Email = _userManager.GetUserName(User);
                usuario = _usuarioDAO.BuscarPorEmail(usuario);
            }
            else
            {
                //não deixar?
            }

            venda.Usuario = usuario;
            venda.ListaBebida = bebidas;


            ItemPizza itemPizza = new ItemPizza();
            itemPizza.Pizza = pizza;
            pizzas.Add(itemPizza);
            venda.ListaPizza = pizzas;

            venda = _VendaDAO.ResbuscarItens(venda);
            _VendaDAO.Cadastrar(venda);

            return RedirectToAction("Index");
        }
    }
}