using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Homies.Models
{
    public class FormEventViewModel
    {
        public FormEventViewModel()
        {
            this.Types = new HashSet<TypeViewModel>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(150,MinimumLength =15)]
        public string Description { get; set; } = null!;

        [Required]
        

        public string Start { get; set; } = null!;

        [Required]
        
        public string End { get; set; } = null!;

        public int TypeId { get; set; }

        public IEnumerable<TypeViewModel> Types { get; set; }

    }
}
