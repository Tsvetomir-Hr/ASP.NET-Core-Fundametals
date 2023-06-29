namespace HouseRentingSystem.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;

    using static HouseRentingSystem.Common.EntityValidationConstants.Agent;

    public class BecomeAgentFormModel
    {

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = null!;
    }
}
