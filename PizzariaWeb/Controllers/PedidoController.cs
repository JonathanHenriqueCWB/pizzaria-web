using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using EcommerceEcoville.Utils;
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

        public PedidoController(TamanhoDAO tamanhoDAO, SaborDAO saborDAO, BebidaDAO bebidaDAO, VendaDAO vendaDAO, UtilsSession utilsSession)
        {
            pizzas = new List<ItemPizza>();

            venda = new Venda();
            pizza = new Pizza();
            _tamanhoDAO = tamanhoDAO;
            _saborDAO = saborDAO;
            _bebidaDAO = bebidaDAO;
            _VendaDAO = vendaDAO;
            _utilsSession = utilsSession;
        }
        #endregion

        public IActionResult Index()
        {
            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());
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
            /*
            pizzas = JsonConvert.DeserializeObject<List<ItemPizza>>(_utilsSession.RetonarPizzas());

            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());
            ItemPizza p = new ItemPizza();
            p.Pizza = pizza;

            pizzas.Add(p);

            _utilsSession.AtualizarPizzas(pizzas);
            */
            return RedirectToAction("Index");

        }

        public IActionResult TerminarPedido()
        {
            bebidas = JsonConvert.DeserializeObject<List<ItemBebida>>(_utilsSession.RetonarBebidas());
            pizza = JsonConvert.DeserializeObject<Pizza>(_utilsSession.RetonarPizza());

            //pizza > listaSabores, tamanho
            //venda > lista de ItemBebida > bebida
            //venda > lista de itemPizza                       
            
            foreach (ItemBebida b in bebidas)
            {
                ItemBebida item = new ItemBebida();
                item.Bebida = _bebidaDAO.BuscarPorId(b.Bebida.BebidaId);

                venda.ListaBebida.Add(item);

                item = null;
            }

            Pizza p = new Pizza();

            foreach (ItemSabor s in pizza.itemSabores)
            {
                ItemSabor item = new ItemSabor();
                item.Sabor = _saborDAO.BuscarPorId(s.Sabor.SaborId);
                p.itemSabores.Add(item);

                item = null;
            }

            venda.ListaBebida = bebidas;

            ItemPizza itemPizza = new ItemPizza();
            itemPizza.Pizza = p;
            pizzas.Add(itemPizza);
            venda.ListaPizza = pizzas;

            _VendaDAO.Cadastrar(venda);

            return RedirectToAction("Index");
        }
    }
}