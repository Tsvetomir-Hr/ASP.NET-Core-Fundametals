using Microsoft.AspNetCore.Identity;

namespace Watchlist.Data
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UsersMovies = new HashSet<UserMovie>();
        }
        public virtual ICollection<UserMovie> UsersMovies { get; set; } = null!;
    }
}
