using System.ComponentModel.DataAnnotations;

namespace Watchlist.Web.Models
{
    public class AddMovieViewModel
    {
        public AddMovieViewModel()
        {
            this.Genres = new HashSet<GenreViewModel>();
        }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }
    }
}
