using System.ComponentModel.DataAnnotations;

namespace WebShopDemo.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [Compare(nameof(PasswordRepeat))]
        public string Password { get; set; } = null!;

        [Required]
        public string PasswordRepeat { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; } = null!;
    }
}
