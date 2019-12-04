using Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceEcoville.Utils
{
    public class UtilsSession
    {
        private readonly IHttpContextAccessor _http;

        private List<ItemBebida> BEBIDAS = new List<ItemBebida>();
        private string BEBIDAS_ID = "Bebidas_id";

        private  List<ItemPizza> PIZZAS = new List<ItemPizza>();
        private string PIZZAS_ID = "Pizzas_id";

        private Pizza PIZZA = new Pizza();
        private string PIZZA_ID = "Pizza_id";

        private double PRECO = 0;
        private string PRECO_ID = "Preco_id";

        public UtilsSession(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string RetonarBebidas()
        {
            if (_http.HttpContext.Session.
                GetString(BEBIDAS_ID) == null)
            {
                _http.HttpContext.Session.SetString(BEBIDAS_ID, JsonConvert.SerializeObject(BEBIDAS));
            }
            return _http.HttpContext.Session.GetString(BEBIDAS_ID);
        }

        public void AtualizarBebida(List<ItemBebida> bebidas)
        {
            BEBIDAS = bebidas;

            if (_http.HttpContext.Session.GetString(BEBIDAS_ID) != null)
            {
                _http.HttpContext.Session.SetString(BEBIDAS_ID, JsonConvert.SerializeObject(BEBIDAS));
            }
        }

        public string RetonarPizza()
        {
            if (PIZZA == null)
            {
                PIZZA = new Pizza();
            }

            if (_http.HttpContext.Session.
                GetString(PIZZA_ID) == null)
            {
                _http.HttpContext.Session.SetString(PIZZA_ID, JsonConvert.SerializeObject(PIZZA));
            }
            return _http.HttpContext.Session.GetString(PIZZA_ID);
        }

        public void AtualizarPizza(Pizza pizza)
        {
            if (PIZZA == null)
            {
                PIZZA = new Pizza();
            }

            PIZZA = pizza;

            if (_http.HttpContext.Session.GetString(PIZZA_ID) != null)
            {
                _http.HttpContext.Session.SetString(PIZZA_ID, JsonConvert.SerializeObject(PIZZA));
            }

        }
        public string RetonarPizzas()
        {
            if (_http.HttpContext.Session.
                GetString(PIZZAS_ID) == null)
            {
                _http.HttpContext.Session.SetString(PIZZAS_ID, JsonConvert.SerializeObject(PIZZAS));
            }
            return _http.HttpContext.Session.GetString(PIZZAS_ID);
        }

        public void AtualizarPizzas(List<ItemPizza> pizzas)
        {
            PIZZAS = pizzas;
            PIZZA = new Pizza();

            AtualizarPizza(PIZZA);

            if (_http.HttpContext.Session.GetString(PIZZAS_ID) != null)
            {
                _http.HttpContext.Session.SetString(PIZZAS_ID, JsonConvert.SerializeObject(PIZZAS));
            }

        }

        public string RetonarPreco()
        {

            if (_http.HttpContext.Session.
                GetString(PRECO_ID) == null)
            {
                _http.HttpContext.Session.SetString(PRECO_ID, JsonConvert.SerializeObject(PRECO));
            }
            return _http.HttpContext.Session.GetString(PRECO_ID);
        }

        public void AtualizarPreco(double preco)
        {
            PRECO += preco;

            if (_http.HttpContext.Session.GetString(PIZZAS_ID) != null)
            {
                _http.HttpContext.Session.SetString(PRECO_ID, JsonConvert.SerializeObject(PRECO));
            }
        }
    }
}
