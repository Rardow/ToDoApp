namespace ToDo.Services.Models
{
    /// <summary>
    /// Search modell for ToDoItems
    /// </summary>
    public class ToDoItemSearchModel
    {
        /// <summary>
        /// Search term
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Status filter
        /// </summary>
        public bool? IsCompleted { get; set; }
    }
}
