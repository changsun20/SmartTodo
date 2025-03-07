using Todo.Core.Models;

namespace Todo.Core.Interfaces;

public interface ITodoService
{
    TodoItem AddTodo(string title);
    void DeleteTodo(int id);
    TodoItem UpdateTitle(int id, string newTitle);
    TodoItem ToggleCompletion(int id);
    TodoItem? GetById(int id);
    IReadOnlyList<TodoItem> GetAllTodos();
    void ClearAllTodos();
}