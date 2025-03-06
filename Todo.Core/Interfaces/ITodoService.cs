using Todo.Core.Models;

namespace Todo.Core.Interfaces;

public interface ITodoService
{
    TodoItem AddTodo(string title);
    void DeleteTodo(int id);
    TodoItem UpdateTitle(int id, string newTitle);
    TodoItem ToggleCompletion(int id); // 新增状态切换
    TodoItem? GetById(int id); // 新增查询单个
    IReadOnlyList<TodoItem> GetAllTodos();
}