﻿using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models_Entities_
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; }
    }
}
