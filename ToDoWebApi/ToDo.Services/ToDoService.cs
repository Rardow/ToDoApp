using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DAL;
using ToDo.DAL.Models;
using ToDo.Services.Interfaces;
using ToDo.Services.Models;

namespace ToDo.Services
{
    /// <summary>
    /// Implementation for the IToDoService
    /// </summary>
    public class ToDoService : IToDoService
    {
        private readonly ILogger<ToDoService> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Constructor for the service
        /// </summary>
        /// <param name="applicationDbContext">DbContext of the application</param>
        public ToDoService(ILogger<ToDoService> logger, ApplicationDbContext applicationDbContext)
        {
            this._logger = logger;
            this._applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Get ToDoItem with id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Item with the given id</returns>
        public ToDoItem GetById(int id)
        {
            try
            {
                return this._applicationDbContext.ToDoItems.SingleOrDefault(t => t.Id == id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Add new ToDoItem 
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Item with the given id</returns>
        public async Task<ToDoItem> AddToDoItem(ToDoItem item)
        {
            try
            {
                this._applicationDbContext.ToDoItems.Add(item);
                var result = await this._applicationDbContext.SaveChangesAsync();
                return result > 0 ? item : null;
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Modidfy ToDoItem 
        /// </summary>
        /// <param name="item">Modified item</param>
        /// <returns>Saved and modified item</returns>
        public async Task<ToDoItem> ModifyToDoItem(ToDoItem item)
        {
            try
            {
                this._applicationDbContext.ToDoItems.Update(item);
                var result = await this._applicationDbContext.SaveChangesAsync();
                return result > 0 ? item : null;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Remove ToDoItem
        /// </summary>
        /// <param name="id">Id of the ToDoItem</param>
        /// <returns>True if success, false if failed</returns>
        public async Task<bool?> RemoveToDoItem(int id)
        {
            try
            {
                if (this._applicationDbContext.ToDoItems.Any(t => t.Id == id))
                {
                    this._applicationDbContext.Remove(this._applicationDbContext.ToDoItems.First(t => t.Id == id));
                    await this._applicationDbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Search in the ToDoItems table
        /// </summary>
        /// <param name="pageIndex">Index of current page</param>
        /// <param name="pageSize">Size of pages</param>
        /// <param name="searchModel">Model for search text and status</param>
        /// <returns>List of ToDoItems</returns>
        public PagedList<ToDoItem> SearchToDoItems(int pageIndex, int pageSize, ToDoItemSearchModel searchModel)
        {
            try
            {
                var result = new PagedList<ToDoItem>();
                var queryItems = this._applicationDbContext.Set<ToDoItem>().Where(t =>
                    t.Note.Contains(searchModel.SearchText ?? string.Empty) &&
                    t.IsCompleted == (searchModel.IsCompleted ?? t.IsCompleted));
                result.PageCount = (int) Math.Ceiling( queryItems.Count() / (double) pageSize );
                result.CurrentPage = pageIndex;
                result.Items = queryItems.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return result;
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Get all record from the ToDoItems table
        /// </summary>
        /// <returns>List of ToDoItems</returns>
        public IEnumerable<ToDoItem> GetAllToDoItems()
        {
            try
            {
                return this._applicationDbContext.ToDoItems;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
