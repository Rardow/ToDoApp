using Microsoft.EntityFrameworkCore;
using ToDo.DAL.Models;

namespace ToDo.DAL
{
    /// <summary>
    /// DbContext for the ToDo application
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor for ApplicationDbContext
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Collection for ToDoItems
        /// </summary>
        public virtual DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
