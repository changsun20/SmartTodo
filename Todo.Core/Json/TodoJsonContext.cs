// Todo.Core/Json/TodoJsonContext.cs
using System.Text.Json.Serialization;
using Todo.Core.Models;

namespace Todo.Core.Json;

[JsonSerializable(typeof(List<TodoItem>))]
[JsonSerializable(typeof(TodoItem))]
[JsonSerializable(typeof(IdWrapper))]
internal partial class TodoJsonContext : JsonSerializerContext
{
}

// Wrapper class used for storing and loading last ID
public class IdWrapper
{
    public int Id { get; set; }
}