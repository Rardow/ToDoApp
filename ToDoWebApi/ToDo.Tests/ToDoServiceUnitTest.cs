using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ToDo.DAL;
using ToDo.DAL.Models;
using ToDo.Services;
using Xunit;

namespace ToDo.Tests
{
    /// <summary>
    /// Unit tests for the ToDoService
    /// </summary>
    public class ToDoServiceUnitTest
    {
        /// <summary>
        /// Testing the GetById method in the ToDoService
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_ToDoItem_ReturnsItemWithGivenId()
        {
            //arrange
            var logger = new LoggerFactory().CreateLogger<ToDoService>();
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ToDo_GetById_TestDB");
            using (var database = new ApplicationDbContext(dbOptions.Options))
            {
                database.Set<ToDoItem>().Add(new ToDoItem()
                {
                    Id = 1,
                    Note = "Test note",
                    IsCompleted = false
                });
                await database.SaveChangesAsync();
            }
            
            using (var database = new ApplicationDbContext(dbOptions.Options))
            {
                //act
                var service = new ToDoService(logger, database);
                var result = service.GetById(1);

                //assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }

        /// <summary>
        /// Testing the RemoveToDoItem method in the ToDoService 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Remove_ToDoItem_ReturnsFalse()
        {
            //arrange
            var logger = new LoggerFactory().CreateLogger<ToDoService>();
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ToDo_Remove_TestDB");
            using (var database = new ApplicationDbContext(dbOptions.Options))
            {
                database.Set<ToDoItem>().Add(new ToDoItem()
                {
                    Id = 1,
                    Note = "Test note",
                    IsCompleted = false
                });
                await database.SaveChangesAsync();
            }

            using (var database = new ApplicationDbContext(dbOptions.Options))
            {
                //act
                var service = new ToDoService(logger, database);
                var result = await service.RemoveToDoItem(2);

                //assert
                Assert.NotNull(result);
                Assert.Equal(false, result);
            }
        }
    }
}
