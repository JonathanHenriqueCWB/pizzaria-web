using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;

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
        [HttpPost]
        public IActionResult Index(Cargo c)
        {
            _cargoDAO.Cadastrar(c);
            ViewBag.ListaCargo = _cargoDAO.Listar();
            return View();
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