using Todo.Core.Models;
using Todo.Core.Interfaces;

namespace Todo.Core.Services;

public class TodoService : ITodoService
{
    private readonly List<TodoItem> _todos = [];
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

    public void DeleteTodo(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null)
            throw new KeyNotFoundException($"Todo with ID {id} not found");

        _todos.Remove(item);
    }

    public TodoItem UpdateTitle(int id, string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("Title cannot be empty");

        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null)
            throw new KeyNotFoundException($"Todo with ID {id} not found");

        item.Title = newTitle.Trim();
        return item;
    }

    public TodoItem ToggleCompletion(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null)
            throw new KeyNotFoundException($"Todo with ID {id} not found");

        item.IsCompleted = !item.IsCompleted;
        return item;
    }

    public TodoItem? GetById(int id) =>
        _todos.FirstOrDefault(t => t.Id == id);

    public IReadOnlyList<TodoItem> GetAllTodos() => _todos.AsReadOnly();
}