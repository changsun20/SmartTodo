using Todo.Core.Models;

namespace Todo.Core.Interfaces;

public interface ITodoService
{
    TodoItem AddTodo(string title);
    IEnumerable<TodoItem> GetAllTodos();
}