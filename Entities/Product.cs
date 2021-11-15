using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend1_2.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool InStock { get; set; }

        [InverseProperty(nameof(OrderLine.Product))]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
