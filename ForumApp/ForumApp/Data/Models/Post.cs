using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ForumApp.Constants.DataConstants.Post;
namespace ForumApp.Data.Models
{
    [Comment("Published posts")]
    public class Post
    {
        [Key]
        [Comment("Post identifier")]
        public int Id { get; set; }

        [Comment("Post title")]
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Comment("Post content")]
        [Required]
        [MaxLength(ContextMaxLength)]
        public string Content { get; set; } = null!;

        [Comment("Marks record is deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
