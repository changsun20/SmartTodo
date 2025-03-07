// Todo.Core/Services/TodoService.cs
using Todo.Core.Models;
using Todo.Core.Interfaces;

namespace Todo.Core.Services;

public class TodoService : ITodoService
{
    private readonly List<TodoItem> _todos = [];
    private int _nextId = 1;
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
        LoadTodos();
    }

    private void LoadTodos()
    {
        // Load tasks from the repository
        var todos = _repository.LoadTodos();
        if (todos != null)
        {
            _todos.Clear();
            _todos.AddRange(todos);
        }

        // Load the last used Id
        _nextId = _repository.GetLastUsedId();
    }

    private void SaveTodos()
    {
        _repository.SaveTodos(_todos);
        _repository.SaveLastUsedId(_nextId);
    }

    public TodoItem AddTodo(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        var newItem = new TodoItem
        {
            Id = _nextId++,
            Title = title.Trim(),
            CreatedAt = DateTime.Now
        };

        _todos.Add(newItem);
        SaveTodos();
        return newItem;
    }

    public void DeleteTodo(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null)
            throw new KeyNotFoundException($"Todo with ID {id} not found");

        _todos.Remove(item);
        SaveTodos();
    }

    public TodoItem UpdateTitle(int id, string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("Title cannot be empty");

        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null)
            throw new KeyNotFoundException($"Todo with ID {id} not found");

        item.Title = newTitle.Trim();
        SaveTodos();
        return item;
    }

    public TodoItem ToggleCompletion(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null)
            throw new KeyNotFoundException($"Todo with ID {id} not found");

        item.IsCompleted = !item.IsCompleted;
        SaveTodos();
        return item;
    }

    public TodoItem? GetById(int id) =>
        _todos.FirstOrDefault(t => t.Id == id);

    public IReadOnlyList<TodoItem> GetAllTodos() => _todos.AsReadOnly();

    public void ClearAllTodos()
    {
        _todos.Clear();
        // Not reset _nextId, to keep the consistency
        SaveTodos();
    }
}