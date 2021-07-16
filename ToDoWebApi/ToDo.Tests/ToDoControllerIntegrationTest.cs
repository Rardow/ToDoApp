using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDo.DAL;
using ToDo.DAL.Models;
using ToDo.Services;
using ToDo.Services.Interfaces;
using Xunit;

namespace ToDo.Tests
{
    /// <summary>
    /// ToDoController integration tests
    /// </summary>
    public class ToDoControllerIntegrationTest : IClassFixture<WebApplicationFactory<ToDoWebApi.Startup>>
    {
        private readonly WebApplicationFactory<ToDoWebApi.Startup> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory"></param>
        public ToDoControllerIntegrationTest(WebApplicationFactory<ToDoWebApi.Startup> factory)
        {
            this._factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IToDoService));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(opts =>
                    {
                        opts.UseInMemoryDatabase("Integration_TestDB");
                    });
                    services.AddScoped<IToDoService, ToDoService>();

                    var serviceProvider = services.BuildServiceProvider();
                    using(var scope = serviceProvider.CreateScope())
                    {
                        var database = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        database.Database.EnsureCreated();

                        database.Set<ToDoItem>().Add(new ToDoItem()
                        {
                            Note = "Test note",
                            IsCompleted = false
                        });
                        database.SaveChanges();
                    }
                });
            });
        }

        /// <summary>
        /// Test HttpGet endpoint return types
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("/ToDo/GetToDoById?id=1")]
        [InlineData("/ToDo/GetAllToDos")]
        public async Task GetEndpoints_ReturnCorrectContentType(string url)
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync(url);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.Mime.MediaTypeNames.Application.Json + "; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        /// <summary>
        /// Test GetToDoById endpoint result
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetToDoById_ReturnCorrectToDoItem()
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync("/ToDo/GetToDoById?id=1");

            //assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var toDoItem = JsonConvert.DeserializeObject<ToDoItem>(content);

            Assert.Equal(System.Net.Mime.MediaTypeNames.Application.Json + "; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(1, toDoItem.Id);
        }

        /// <summary>
        /// Test AddToDo endpoint
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddToDo_ReturnToDoItemWithId()
        {
            //arrange
            var client = _factory.CreateClient();
            var jsonString = JsonConvert.SerializeObject(new ToDoItem() { Note = "Note 2", IsCompleted = true });
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            //act
            var response = await client.PostAsync("/ToDo/AddToDo", httpContent);

            //assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var toDoItem = JsonConvert.DeserializeObject<ToDoItem>(content);

            Assert.Equal(System.Net.Mime.MediaTypeNames.Application.Json + "; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.NotNull(toDoItem);
            Assert.NotEqual(0, toDoItem.Id);
        }
    }
}
