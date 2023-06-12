using System.ComponentModel.DataAnnotations;

namespace Watchlist.Data.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }

    }
}
