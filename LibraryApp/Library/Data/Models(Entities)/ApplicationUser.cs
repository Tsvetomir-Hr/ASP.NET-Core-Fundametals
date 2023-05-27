using Microsoft.AspNetCore.Identity;

namespace Library.Data.Models_Entities_
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.ApplicationUsersBooks = new HashSet<ApplicationUserBook>();
        }
        public virtual ICollection<ApplicationUserBook> ApplicationUsersBooks { get; set; } 
    }
}
