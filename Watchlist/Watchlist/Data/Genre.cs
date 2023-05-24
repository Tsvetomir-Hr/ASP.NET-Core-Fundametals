using System.ComponentModel.DataAnnotations;

namespace Watchlist.Data
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
