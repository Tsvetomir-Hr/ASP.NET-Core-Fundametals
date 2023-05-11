using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
        public string Title { get; set; }

        [Comment("Post content")]
        [Required]
        public string Content { get; set; }
    }
}
