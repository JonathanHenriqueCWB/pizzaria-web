using Domain.Models;
using Repository.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class CargoDAO : IRepository<Cargo>
    {
        #region Configuração contexto
        private readonly PizzariaContext _context;
        public CargoDAO(PizzariaContext context)
        {
            _context = context;
        }
        #endregion

        public Cargo BuscarPorId(int? id)
        {
            return _context.Cargos.Find(id);
        }

        public bool Cadastrar(Cargo objeto)
        {
            _context.Cargos.Add(objeto);
            _context.SaveChanges();
            return true;
        }

        public List<Cargo> Listar()
        {
            return _context.Cargos.ToList();
        }
        public void Remover(int id)
        {
            _context.Cargos.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
    }
}
