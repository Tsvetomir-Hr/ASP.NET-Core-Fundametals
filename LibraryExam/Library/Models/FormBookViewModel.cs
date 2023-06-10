using Library.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class FormBookViewModel
    {
        public FormBookViewModel()
        {
            this.Categories = new HashSet<Category>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =10)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal),"0.00","10.00")]
        public decimal Rating { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = null!;

    }
}
