using Todo.Core.Models;

namespace Todo.Core.Interfaces;

public interface ITodoRepository
{
    bool SaveTodos(IEnumerable<TodoItem> todos);
    IEnumerable<TodoItem>? LoadTodos();
    int GetLastUsedId();
    void SaveLastUsedId(int id);
}