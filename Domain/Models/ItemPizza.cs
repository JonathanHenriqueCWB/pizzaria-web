using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("ItemPizzas")]
    public class ItemPizza
    {
        [Key]
        public int ItemPizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public double Preco { get; set; }
    }
}
