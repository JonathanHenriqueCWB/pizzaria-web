using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("Pizzas")]
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }
        public Tamanho Tamanho { get; set; }
        public List<ItemSabor> itemSabores { get; set; }

        public Pizza()
        {
            Tamanho = new Tamanho();
            itemSabores = new List<ItemSabor>();
        }
    }
}
