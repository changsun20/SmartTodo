using Todo.Core.Services;

var todoService = new TodoService();
Console.WriteLine("Welcome to SmartTodo CLI!");
Console.WriteLine("Available commands:");
Console.WriteLine("  add [title]  - Add new todo");
Console.WriteLine("  list         - Show all todos");
Console.WriteLine("  exit         - Exit the program\n");

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(input)) continue;

    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (input.StartsWith("add ", StringComparison.OrdinalIgnoreCase))
    {
        var title = input[4..].Trim();
        if (title.Length == 0)
        {
            Console.WriteLine("Invalid command. Usage: add [title]");
            continue;
        }

        try
        {
            var newItem = todoService.AddTodo(title);
            Console.WriteLine($"Added todo #{newItem.Id}: {newItem.Title}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    else if (input.Equals("list", StringComparison.OrdinalIgnoreCase))
    {
        var todos = todoService.GetAllTodos();
        Console.WriteLine(todos.Any()
            ? "Current todos:\n" + string.Join("\n", todos.Select(t => $"#{t.Id} {t.Title}"))
            : "No todos yet!");
    }
    else
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  add [title]  - Add new todo");
        Console.WriteLine("  list         - Show all todos");
        Console.WriteLine("  exit         - Exit the program");
    }
}

Console.WriteLine("Goodbye!");