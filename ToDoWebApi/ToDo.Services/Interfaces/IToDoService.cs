using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.DAL.Models;
using ToDo.Services.Models;

namespace ToDo.Services.Interfaces
{
    /// <summary>
    /// Interface for the ToDoItem actions
    /// </summary>
    public interface IToDoService
    {
        /// <summary>
        /// Get ToDoItem with id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Item with the given id</returns>
        ToDoItem GetById(int id); 
        
        /// <summary>
        /// Add new ToDoItem 
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Item with the given id</returns>
        Task<ToDoItem> AddToDoItem(ToDoItem item);

        /// <summary>
        /// Modidfy ToDoItem 
        /// </summary>
        /// <param name="item">Modified item</param>
        /// <returns>Saved and modified item</returns>
        Task<ToDoItem> ModifyToDoItem(ToDoItem item);

        /// <summary>
        /// Remove ToDoItem
        /// </summary>
        /// <param name="id">Id of the ToDoItem</param>
        /// <returns>True if success, false if failed</returns>
        Task<bool?> RemoveToDoItem(int id);

        /// <summary>
        /// Search in the ToDoItems table
        /// </summary>
        /// <param name="searchModel">Model for search text and status</param>
        /// <returns>List of ToDoItems</returns>
        PagedList<ToDoItem> SearchToDoItems(int pageIndex, int pageSize, ToDoItemSearchModel searchModel);

        /// <summary>
        /// Get all record from the ToDoItems table
        /// </summary>
        /// <returns>List of ToDoItems</returns>
        IEnumerable<ToDoItem> GetAllToDoItems();
    }
}
