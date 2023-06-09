using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities
{
    public class Book
    {
        public Book()
        {
            this.IdentityUserBook = new HashSet<IdentityUserBook>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public decimal Rating { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [Required]
        public virtual Category Category { get; set; }

        public virtual ICollection<IdentityUserBook> IdentityUserBook { get; set; }
    }
}
