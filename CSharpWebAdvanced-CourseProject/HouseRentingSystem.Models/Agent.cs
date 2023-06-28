
namespace HouseRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static HouseRentingSystem.Common.EntityValidationConstants.Agent;

    public class Agent
    {
        public Agent()
        {
            this.OwnedHouses = new HashSet<House>();k
            this.OwnedHouses = new HashSet<House>();k
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumer { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<House> OwnedHouses { get; set; }
    }
}
