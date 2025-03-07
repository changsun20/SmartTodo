using Todo.Core.Services;
using Todo.Core.Models;


namespace Todo.Core.Tests.Services
{
    public class JsonTodoRepositoryTests
    {
        private readonly string _tempDir;
        private readonly JsonTodoRepository _repository;

        public JsonTodoRepositoryTests()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), $"SmartTodoRepoTest_{Guid.NewGuid()}");
            Directory.CreateDirectory(_tempDir);
            _repository = new JsonTodoRepository(_tempDir);
        }

        [Fact]
        public void SaveAndLoadTodos_PersistsDataCorrectly()
        {
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Task 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Task 2", IsCompleted = true }
            };
            _repository.SaveTodos(todos);

            var loadedTodos = _repository.LoadTodos()?.ToList();
            Assert.NotNull(loadedTodos);
            Assert.Equal(2, loadedTodos.Count);
            Assert.Equal("Task 1", loadedTodos[0].Title);
            Assert.Equal("Task 2", loadedTodos[1].Title);
            Assert.True(loadedTodos[1].IsCompleted);
        }

        [Fact]
        public void LoadTodos_FileDoesNotExist_ReturnsNull()
        {
            var newRepo = new JsonTodoRepository(Path.Combine(_tempDir, "nonexistent"));
            Assert.Null(newRepo.LoadTodos());
        }

        [Fact]
        public void GetLastUsedId_DefaultsToOneWhenFileMissing()
        {
            var newRepo = new JsonTodoRepository(Path.Combine(_tempDir, "missing_id"));
            Assert.Equal(1, newRepo.GetLastUsedId());
        }

        [Fact]
        public void SaveAndLoadLastUsedId_PersistsCorrectly()
        {
            _repository.SaveLastUsedId(42);
            Assert.Equal(42, _repository.GetLastUsedId());
        }
    }
}