using Todo.Cli.Utils;
using Todo.Core.Services;

var todoRepository = new JsonTodoRepository();
var todoService = new TodoService(todoRepository);
var commandHandler = new CommandHandler(todoService);

Console.WriteLine("Welcome to SmartTodo CLI!");
CommandHandler.ShowHelp();

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(input)) continue;

    var (command, arguments) = ParseInput(input);

    switch (command.ToLower())
    {
        case "exit":
            Console.WriteLine("Goodbye!");
            return;

        case "add":
            commandHandler.HandleAdd(arguments);
            break;

        case "list":
            commandHandler.HandleList();
            break;

        case "delete":
            commandHandler.HandleDelete(arguments);
            break;

        case "update":
            commandHandler.HandleUpdate(arguments);
            break;

        case "complete":
            commandHandler.HandleComplete(arguments);
            break;
            
        case "clear":
            commandHandler.HandleClear();
            break;

        default:
            CommandHandler.ShowHelp();
            break;
    }
}

(string Command, string Args) ParseInput(string input)
{
    var firstSpace = input.IndexOf(' ');
    return firstSpace == -1
        ? (input, string.Empty)
        : (input[..firstSpace], input[(firstSpace + 1)..].Trim());
}