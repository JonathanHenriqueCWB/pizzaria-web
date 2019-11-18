using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("ItemSabores")]
    public class ItemSabor
    {
        [Key]
        public int ItemSaborId { get; set; }
        public Sabor Sabor { get; set; }
    }
}
