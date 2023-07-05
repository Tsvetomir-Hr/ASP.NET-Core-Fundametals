namespace HouseRentingSystem.Web.ViewModels.House
{
    using HouseRentingSystem.Web.ViewModels.Category;
    using System.ComponentModel.DataAnnotations;

    using static HouseRentingSystem.Common.EntityValidationConstants.House;
    using static HouseRentingSystem.Common.EntityValidationConstants.Category;
    public class HouseFormModel
    {
        public HouseFormModel()
        {
            this.Categories = new HashSet<HouseSelectCategoryFormModel>();
        }
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), PricePerMonthMinValue, PricePerMonthMaxValue)]
        public decimal PricePerMonth { get; set; }

        public int CategoryId { get; set; }

        public ICollection<HouseSelectCategoryFormModel> Categories { get; set; }
    }
}
