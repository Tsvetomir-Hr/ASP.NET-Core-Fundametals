using Microsoft.AspNetCore.Identity;

namespace Contacts.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.ApplicationUsersContacts = new HashSet<ApplicationUserContact>();
        }
        public virtual ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}
