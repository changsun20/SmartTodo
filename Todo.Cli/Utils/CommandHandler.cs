using Todo.Core.Interfaces;

namespace Todo.Cli.Utils;

public class CommandHandler(ITodoService todoService)
{
    public void HandleAdd(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine("Invalid command. Usage: add [title]");
            return;
        }

        try
        {
            var newItem = todoService.AddTodo(args);
            Console.WriteLine($"Added todo #{newItem.Id}: {newItem.Title}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void HandleList()
    {
        var todos = todoService.GetAllTodos();

        if (todos.Count == 0)
        {
            Console.WriteLine("No todos yet!");
            return;
        }

        Console.WriteLine($"Current todos ({todos.Count}):");
        foreach (var t in todos)
        {
            var status = t.IsCompleted ? "[x]" : "[ ]";
            Console.WriteLine($" #{t.Id} {status} {t.Title} (Created: {t.CreatedAt:yyyy-MM-dd HH:mm})");
        }
    }

    public void HandleDelete(string args)
    {
        if (!int.TryParse(args, out var id))
        {
            Console.WriteLine("Invalid ID format");
            return;
        }

        try
        {
            todoService.DeleteTodo(id);
            Console.WriteLine($"Deleted todo #{id}");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void HandleUpdate(string args)
    {
        var parts = args.Split(' ', 2);
        if (parts.Length != 2 || !int.TryParse(parts[0], out var id))
        {
            Console.WriteLine("Invalid command. Usage: update [id] [new title]");
            return;
        }

        try
        {
            var updated = todoService.UpdateTitle(id, parts[1]);
            Console.WriteLine($"Updated todo #{id}: {updated.Title}");
        }
        catch (Exception ex) when (ex is KeyNotFoundException || ex is ArgumentException)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void HandleComplete(string args)
    {
        if (!int.TryParse(args, out var id))
        {
            Console.WriteLine("Invalid ID format");
            return;
        }

        try
        {
            var item = todoService.ToggleCompletion(id);
            var status = item.IsCompleted ? "completed" : "pending";
            Console.WriteLine($"Todo #{id} marked as {status}");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void HandleClear()
    {
        try
        {
            todoService.ClearAllTodos();
            Console.WriteLine("All todos have been cleared!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing todos: {ex.Message}");
        }
    }

    public static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  add [title]       - Add new todo");
        Console.WriteLine("  list              - Show all todos");
        Console.WriteLine("  delete [id]       - Remove a todo");
        Console.WriteLine("  update [id] [new] - Update todo title");
        Console.WriteLine("  complete [id]     - Toggle completion status");
        Console.WriteLine("  clear             - Remove all todos");
        Console.WriteLine("  exit              - Exit the program");
    }
}