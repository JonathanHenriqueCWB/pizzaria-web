using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("Vendas")]
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }
        public List<ItemPizza> ListaPizza { get; set; }
        public List<ItemBebida> ListaBebida { get; set; }
        public Usuario Usuario { get; set; }
        public double Preco { get; set; }

        public Venda()
        {
            Usuario = new Usuario();
            ListaPizza = new List<ItemPizza>();
            ListaBebida = new List<ItemBebida>();
        }
    }
}
