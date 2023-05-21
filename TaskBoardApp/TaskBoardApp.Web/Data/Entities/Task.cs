using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardApp.Web.Data.Entities
{
    /// <summary>
    /// Task
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Task identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Task title
        /// </summary>
        [Required]
        [MaxLength(DataConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;
        /// <summary>
        /// Task description
        /// </summary>
        [Required]
        [MaxLength(DataConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;
        /// <summary>
        /// Date and time when is creted the task
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Foreign key to the boards table
        /// </summary>
        
        public int BoardId { get; set; }
        /// <summary>
        /// Board entitiy
        /// </summary>
        public virtual Board Board { get; set; } = null!;
        /// <summary>
        /// Foreign key tot he owners table
        /// </summary>
        // Application users has Guid for id thats why here we have string OwnerId as a Foreign key.
        [ForeignKey(nameof(Owner))]
        [Required]
        public string OwnerId { get; set; } = null!;
        /// <summary>
        /// Owner entity
        /// </summary>
        public virtual ApplicationUser Owner { get; set; } = null!;

    }
}
