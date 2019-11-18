using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("ItemBebidas")]
    public class ItemBebida
    {
        [Key]
        public int ItemBebidaId { get; set; }
        public Bebida Bebida { get; set; }
        public double Preco { get; set; }
    }
}
