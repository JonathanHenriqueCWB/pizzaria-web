using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;

namespace API.Controllers
{
    [Route("api/Bebidas")]
    [ApiController]
    public class BebidaAPIController : ControllerBase
    {
        private readonly BebidaDAO _bebidaDAO;

        public BebidaAPIController(BebidaDAO bebidaDAO)
        {
            _bebidaDAO = bebidaDAO;
        }

        // /api/Bebidas/ListarTodos
        [HttpGet]
        [Route("ListarTodos")]
        public IActionResult ListarTodos()
        {
            return Ok(_bebidaDAO.Listar());
        }

        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Bebida bebida = _bebidaDAO.BuscarPorId(id);

            if (bebida != null)
                return Ok(bebida);

            return NotFound(new { msg = "Bebida não encontrada." });
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Bebida b)
        {
            if (_bebidaDAO.Cadastrar(b))
                return Created("", b);

            return Conflict(new { msg = "Essa bebida já existe."});
        }
    }
}