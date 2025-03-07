// Todo.Core/Services/JsonTodoRepository.cs
using System.Text.Json;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Core.Json;

namespace Todo.Core.Services;

public class JsonTodoRepository : ITodoRepository
{
    private readonly string _filePath;
    private readonly string _idFilePath;
    
    public JsonTodoRepository(string baseDirectory = "")
    {
        // Determine the data directory
        var dataDir = string.IsNullOrEmpty(baseDirectory) 
            ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data") 
            : Path.Combine(baseDirectory, "data");
        
        Directory.CreateDirectory(dataDir);
        
        _filePath = Path.Combine(dataDir, "todos.json");
        _idFilePath = Path.Combine(dataDir, "lastid.json");
    }

    public bool SaveTodos(IEnumerable<TodoItem> todos)
    {
        try
        {
            var todosList = todos.ToList();
            var json = JsonSerializer.Serialize(todosList, TodoJsonContext.Default.ListTodoItem);
            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving todos: {ex.Message}");
            return false;
        }
    }

    public IEnumerable<TodoItem>? LoadTodos()
    {
        if (!File.Exists(_filePath))
            return null;
            
        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize(json, TodoJsonContext.Default.ListTodoItem);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading todos: {ex.Message}");
            return null;
        }
    }

    public int GetLastUsedId()
    {
        if (!File.Exists(_idFilePath))
            return 1;
            
        try
        {
            var json = File.ReadAllText(_idFilePath);
            var idWrapper = JsonSerializer.Deserialize(json, TodoJsonContext.Default.IdWrapper);
            return idWrapper?.Id ?? 1;
        }
        catch (Exception)
        {
            return 1;
        }
    }

    public void SaveLastUsedId(int id)
    {
        try
        {
            var idWrapper = new IdWrapper { Id = id };
            var json = JsonSerializer.Serialize(idWrapper, TodoJsonContext.Default.IdWrapper);
            File.WriteAllText(_idFilePath, json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving last ID: {ex.Message}");
        }
    }
}
