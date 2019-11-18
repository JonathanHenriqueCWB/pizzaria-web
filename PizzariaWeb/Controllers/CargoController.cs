using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;

using Firebase.Database;
using Newtonsoft.Json;

namespace PizzariaWeb.Controllers
{
    public class CargoController : Controller
    {
        #region DAO configuração
        private readonly CargoDAO _cargoDAO;
        public CargoController(CargoDAO cargoDAO)
        {
            _cargoDAO = cargoDAO;
        }
        #endregion
        #region INDEX LISTAR CADASTRAR
        public IActionResult Index(bool isJson)
        {
            List<Cargo> cargos = _cargoDAO.Listar();
            ViewBag.ListaCargo = cargos;

            if (isJson)
                return Json(cargos);

            return View();
        }
        public async Task<JsonResult> IndexFirebase()
        {
            FirebaseClient client = new FirebaseClient("https://pizzaria-f39b1.firebaseio.com/");
            var cargos = await client.Child("Cargo").OnceAsync<Cargo>();

            List<Cargo> list = new List<Cargo>();

            foreach (var item in cargos)
            {
                Cargo x = new Cargo
                {
                    CargoId = item.Object.CargoId,
                    Nome = item.Object.Nome,
                    Salario = item.Object.Salario
                };

                list.Add(x);
            }

            return Json(list);
        }
        [HttpPost]
        public IActionResult Index(Cargo c)
        {
            _cargoDAO.Cadastrar(c);
            ViewBag.ListaCargo = _cargoDAO.Listar();
            return View();
        }
        [HttpPost]
        public async Task<bool> IndexFirebase(Cargo cargo)
        {
            FirebaseClient client = new FirebaseClient("https://pizzaria-f39b1.firebaseio.com/");
            var result = await client.Child("Cargo").PostAsync(JsonConvert.SerializeObject(cargo));

            return true;
        }
        #endregion
        #region REMOVER
        public IActionResult Remover(int id)
        {
            _cargoDAO.Remover(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}