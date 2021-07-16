using System.ComponentModel.DataAnnotations;

namespace ToDo.DAL.Models
{
    /// <summary>
    /// Model for a to-do item
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Status flag
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
