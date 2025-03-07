using Todo.Core.Services;
using Todo.Core.Interfaces;

namespace Todo.Core.Tests.Services
{
    public class TodoServiceTests
    {
        private readonly ITodoRepository _repository;
        private readonly TodoService _service;

        public TodoServiceTests()
        {
            // Use temporary directory for test data
            string tempDir = Path.Combine(Path.GetTempPath(), $"SmartTodoTest_{Guid.NewGuid()}");
            Directory.CreateDirectory(tempDir);
            _repository = new JsonTodoRepository(tempDir);
            _service = new TodoService(_repository);
        }

        [Fact]
        public void AddTodo_ValidTitle_ReturnsTodoWithIncrementedId()
        {
            var todo1 = _service.AddTodo("First item");
            var todo2 = _service.AddTodo("Second item");

            Assert.Equal(1, todo1.Id);
            Assert.Equal(2, todo2.Id);
            Assert.Equal("First item", todo1.Title);
            Assert.False(todo1.IsCompleted);
        }

        [Fact]
        public void AddTodo_EmptyTitle_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.AddTodo("   "));
        }

        [Fact]
        public void GetAllTodos_InitiallyEmpty()
        {
            Assert.Empty(_service.GetAllTodos());
        }

        [Fact]
        public void GetAllTodos_ReturnsAllAddedItems()
        {
            _service.AddTodo("Test 1");
            _service.AddTodo("Test 2");

            var todos = _service.GetAllTodos();

            Assert.Equal(2, todos.Count);
            Assert.Contains(todos, t => t.Title == "Test 1");
            Assert.Contains(todos, t => t.Title == "Test 2");
        }

        [Fact]
        public void DeleteTodo_ValidId_RemovesItem()
        {
            var item = _service.AddTodo("To delete");
            Assert.NotNull(_service.GetById(item.Id));

            _service.DeleteTodo(item.Id);

            Assert.Null(_service.GetById(item.Id));
        }

        [Fact]
        public void DeleteTodo_InvalidId_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _service.DeleteTodo(999));
        }

        [Fact]
        public void UpdateTitle_ValidInput_UpdatesItem()
        {
            var item = _service.AddTodo("Original");
            var updated = _service.UpdateTitle(item.Id, "Updated");

            Assert.Equal("Updated", updated.Title);
            Assert.Equal("Updated", _service.GetById(item.Id)?.Title);
        }

        [Fact]
        public void UpdateTitle_InvalidId_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _service.UpdateTitle(999, "New Title"));
        }

        [Fact]
        public void ToggleCompletion_ValidId_SwitchesStatus()
        {
            var item = _service.AddTodo("Test");
            var firstResult = _service.ToggleCompletion(item.Id);

            Assert.True(firstResult.IsCompleted);

            var secondResult = _service.ToggleCompletion(item.Id);
            Assert.False(secondResult.IsCompleted);
        }

        [Fact]
        public void ToggleCompletion_InvalidId_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _service.ToggleCompletion(999));
        }

        [Fact]
        public void GetById_InvalidId_ReturnsNull()
        {
            Assert.Null(_service.GetById(999));
        }

        [Fact]
        public void ClearAllTodos_RemovesAllItems()
        {
            _service.AddTodo("Item 1");
            _service.AddTodo("Item 2");
            _service.AddTodo("Item 3");
            _service.ClearAllTodos();
            Assert.Empty(_service.GetAllTodos());

            var newItem = _service.AddTodo("New Item");
            Assert.Equal(4, newItem.Id);
        }

        [Fact]
        public void Persistence_Test()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), $"SmartTodoTest_{Guid.NewGuid()}");
            Directory.CreateDirectory(tempDir);
            var repository = new JsonTodoRepository(tempDir);
            var service = new TodoService(repository);

            var addedItem = service.AddTodo("Persistent Task");

            var newService = new TodoService(repository);
            var loadedItem = newService.GetById(addedItem.Id);

            Assert.NotNull(loadedItem);
            Assert.Equal("Persistent Task", loadedItem!.Title);
        }
    }
}
