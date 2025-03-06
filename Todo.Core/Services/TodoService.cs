using Todo.Core.Models;
using Todo.Core.Interfaces;

namespace Todo.Core.Services;

public class TodoService : ITodoService
{
    private readonly List<TodoItem> _todos = new();
    private int _nextId = 1;

    public TodoItem AddTodo(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        var newItem = new TodoItem
        {
            Id = _nextId++,
            Title = title.Trim()
        };

        _todos.Add(newItem);
        return newItem;
    }

    public IReadOnlyList<TodoItem> GetAllTodos() => _todos.AsReadOnly();
}