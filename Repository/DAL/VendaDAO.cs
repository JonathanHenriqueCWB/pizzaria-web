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
        private BebidaDAO _bebidaDAO;
        private TamanhoDAO _tamanhoDAO;
        private SaborDAO _saborDAO;

        private readonly PizzariaContext _context;
        public VendaDAO(PizzariaContext context)
        {
            _context = context;
            _tamanhoDAO = new TamanhoDAO(context);
            _bebidaDAO = new BebidaDAO(context);
            _saborDAO = new SaborDAO(context);
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

        public Venda ResbuscarItens(Venda venda)
        {
            Venda TempVenda = new Venda();

            foreach (ItemBebida itemBebida in venda.ListaBebida)
            {
                ItemBebida TempItemBebida = new ItemBebida();
                TempItemBebida.Bebida = _bebidaDAO.BuscarPorId(itemBebida.Bebida.BebidaId);

                TempVenda.ListaBebida.Add(TempItemBebida);

                TempItemBebida = null;
                TempVenda.Preco += TempItemBebida.Bebida.Preco;
            }

            foreach (ItemPizza itemPizza in venda.ListaPizza)
            {
                Pizza TempPizza = new Pizza();
                TempPizza.Tamanho = _tamanhoDAO.BuscarPorId(itemPizza.Pizza.Tamanho.TamanhoId);

                TempVenda.Preco += TempPizza.Tamanho.Preco;


                foreach (ItemSabor itemSabor in itemPizza.Pizza.itemSabores)
                {
                    ItemSabor item = new ItemSabor();
                    item.Sabor = _saborDAO.BuscarPorId(itemSabor.Sabor.SaborId);
                    TempPizza.itemSabores.Add(item);

                    item = null;
                }

                ItemPizza TempItemPizza = new ItemPizza();
                TempItemPizza.Pizza = TempPizza;
                TempVenda.ListaPizza.Add(TempItemPizza);

                TempItemPizza = null;
                TempPizza = null;
            }

            TempVenda.Usuario = venda.Usuario;
            return TempVenda;
        }

        public List<Venda> Listar()
        {
            return _context.Vendas.ToList();
        }
    }
}
