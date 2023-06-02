using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data.Entities
{
    public class UserMovie
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;
    }
}
