

using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models_Entities_
{
    public class Book
    {
        public Book()
        {
            this.ApplicationUsersBooks = new HashSet<ApplicationUserBook>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = null!;
        [Required]
        [MaxLength(5000)]
        public string Description { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
        [Required]
        public decimal Rating { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public virtual Category Category { get; set; }

        public virtual ICollection<ApplicationUserBook> ApplicationUsersBooks { get; set; }
    }
}
