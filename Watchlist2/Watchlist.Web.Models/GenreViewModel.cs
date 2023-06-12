using System.ComponentModel.DataAnnotations;

namespace Watchlist.Web.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = null!;
    }
}
