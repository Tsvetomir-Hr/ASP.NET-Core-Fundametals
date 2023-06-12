
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data.Models
{
    public class Movie
    {
        public Movie()
        {
            this.UsersMovies = new HashSet<UserMovie>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Director { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
        public decimal Rating { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        [Required]
        public virtual Genre Genre { get; set; } = null!;

        public virtual ICollection<UserMovie> UsersMovies { get; set; }
    }
}
