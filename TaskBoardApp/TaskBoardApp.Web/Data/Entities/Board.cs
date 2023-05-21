using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Web.Data.Entities
{
    /// <summary>
    /// Board entity
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Initializing the collection of task as Hashset
        /// </summary>
        public Board()
        {
            this.Tasks = new HashSet<Task>();
        }
        /// <summary>
        /// Board identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Board name
        /// </summary>
        [Required]
        [MaxLength(DataConstants.BoardNameMaxLength)]
        public string Name { get; set; } = null!;
        /// <summary>
        /// Collection of tasks to this board.
        /// </summary>
        public virtual ICollection<Task> Tasks { get; set; } = null!;
    }
}
