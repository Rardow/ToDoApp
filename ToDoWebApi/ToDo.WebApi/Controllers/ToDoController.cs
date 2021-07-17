using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ToDo.DAL.Models;
using ToDo.Services.Interfaces;
using ToDo.Services.Models;

namespace ToDoWebApi.Controllers
{
    /// <summary>
    /// Controller for the ToDoApplication main functions
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoService _toDoService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="toDoService"></param>
        public ToDoController(ILogger<ToDoController> logger, IToDoService toDoService)
        {
            this._logger = logger;
            this._toDoService = toDoService;
        }

        /// <summary>
        /// Get a ToDoItem with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ToDoItem> GetToDoById(int id)
        {
            try
            {
                var result = _toDoService.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all ToDoItems
        /// </summary>
        /// <param name="searchModel">Search model with search text and status</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<PagedList<ToDoItem>> GetAllToDos()
        {
            try
            {
                var result = _toDoService.GetAllToDoItems();
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Search ToDoItems
        /// </summary>
        /// <param name="searchModel">Search model with search text and status</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<PagedList<ToDoItem>> SearchToDos([FromQuery] ToDoItemSearchModel searchModel, int pageIndex = 1, int pageSize = 25)
        {
            try
            {
                var result = _toDoService.SearchToDoItems(pageIndex, pageSize > 25 ? 25 : pageSize, searchModel);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Add new ToDoItem to the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ToDoItem>> AddToDo([FromBody] ToDoItem item)
        {
            try
            {
                var result = await _toDoService.AddToDoItem(item);
                if(result == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return BadRequest();
            }            
        }

        /// <summary>
        /// Modify ToDoItem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> ModifyToDo([FromBody] ToDoItem item)
        {
            try
            {
                var result = await _toDoService.ModifyToDoItem(item);
                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Remove ToDoItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<bool>> RemoveToDo(int id)
        {
            try
            {
                var result = await _toDoService.RemoveToDoItem(id);
                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    return result.GetValueOrDefault() ? Ok(result) : NotFound(result);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
