using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities
{
    public class IdentityUserBook
    {
        [Required]
        [ForeignKey(nameof(Collector))]
        public string CollectorId { get; set; } = null!;
        public virtual IdentityUser Collector { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
