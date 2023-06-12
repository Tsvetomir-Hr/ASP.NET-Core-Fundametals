using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data.Models
{
    public class UserMovie
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
