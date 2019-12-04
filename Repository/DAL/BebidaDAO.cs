using Domain.Models;
using Repository.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.DAL
{
    public class BebidaDAO : IRepository<Bebida>
    {
        #region Configuração contexto
        private readonly PizzariaContext _context;
        public BebidaDAO(PizzariaContext context)
        {
            _context = context;
        }
        #endregion

        public Bebida BuscarPorId(int? id)
        {
            return _context.Bebidas.Find(id);
        }

        public bool Cadastrar(Bebida objeto)
        {
            _context.Bebidas.Add(objeto);
            _context.SaveChanges();
            return true;
        }

        public List<Bebida> Listar()
        {
            return _context.Bebidas.Where(x => x.Desabilitado == false).ToList();
        }
        public void Remover(Bebida bebida)
        {
            bebida.Desabilitado = true;
            _context.Bebidas.Update(bebida);
            //_context.Bebidas.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
        public void Alterar(Bebida b)
        {
            _context.Bebidas.Update(b);
            _context.SaveChanges();
        }
    }
}
