using Contacts.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class FormContactViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(60, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(17, MinimumLength = 10)]
        [RegularExpression("(?:\\+?\\d{1,3}\\s?)?(?:-?\\d{2,4}\\s?){4}")]
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        [Required]
        [RegularExpression(@"(w{3}.)[A-z0-9\-]+(.bg)")]
        public string WebSite { get; set; } = null!;

        
    }
}
