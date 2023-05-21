using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Web.Data.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User FirstName
        /// </summary>
        [Required]
        [MaxLength(DataConstants.UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        /// <summary>
        /// User LastName
        /// </summary>
        [Required]
        [MaxLength(DataConstants.UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;
    }
}
