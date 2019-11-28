using Domain.Models;
using Repository.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.DAL
{
    public class VendaDAO : IRepository<Venda>
    {
        #region Configuração contexto
        private readonly PizzariaContext _context;
        public VendaDAO(PizzariaContext context)
        {
            _context = context;
        }
        #endregion

        public Venda BuscarPorId(int? id)
        {
            return _context.Vendas.Find(id);
        }

        public bool Cadastrar(Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return true;
        }

        public List<Venda> Listar()
        {
            return _context.Vendas.ToList();
        }
    }
}
