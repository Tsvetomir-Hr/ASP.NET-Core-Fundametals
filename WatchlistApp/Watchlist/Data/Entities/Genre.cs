using System.ComponentModel.DataAnnotations;

namespace Watchlist.Data.Entities
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
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
