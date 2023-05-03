using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDemo.Core.Data.Models
{
    [Comment("Product to sell")]
    public class Product
    {
        [Key]
        [Comment("Unique identifier")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Comment("Product name")]
        public string Name { get; set; } = null!;

        [Comment("Product price")]
        public decimal Price { get; set; }

        [Comment("Product quantity")]
        public int Quantity { get; set; }
    }
}
