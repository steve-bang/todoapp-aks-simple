
using System.Diagnostics.CodeAnalysis;

namespace ToDoApi.Models;

public class Todo 
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public Todo() {}

    public static Todo Create(string name)
    {
        return new Todo()
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsCompleted = false
        };
    }

    public void MarkCompleted()
    {
        IsCompleted = true;
    }
}