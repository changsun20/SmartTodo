using Todo.Core.Services;

namespace Todo.Core.Tests.Services;

public class TodoServiceTests
{
    private readonly TodoService _service = new();

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
    public void GetAllTodos_ReturnsAllAddedItems()
    {
        _service.AddTodo("Test 1");
        _service.AddTodo("Test 2");

        var todos = _service.GetAllTodos();

        Assert.Equal(2, todos.Count());
        Assert.Contains(todos, t => t.Title == "Test 1");
        Assert.Contains(todos, t => t.Title == "Test 2");
    }

    [Fact]
    public void DeleteTodo_ValidId_RemovesItem()
    {
        var item = _service.AddTodo("To delete");

        _service.DeleteTodo(item.Id);

        Assert.DoesNotContain(item, _service.GetAllTodos());
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
    public void ToggleCompletion_ValidId_SwitchesStatus()
    {
        var item = _service.AddTodo("Test");
        
        var firstResult = _service.ToggleCompletion(item.Id);
        Assert.True(firstResult.IsCompleted);
        
        var secondResult = _service.ToggleCompletion(item.Id);
        Assert.False(secondResult.IsCompleted);
    }

    [Fact]
    public void GetById_InvalidId_ReturnsNull()
    {
        Assert.Null(_service.GetById(999));
    }
}