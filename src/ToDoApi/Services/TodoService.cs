
using Microsoft.EntityFrameworkCore;
using TodoApi.Context;
using ToDoApi.Models;

namespace ToDoApi.Services;

public class TodoService(TodoContext TodoContext) : ITodoService
{

    public Todo Create(Todo data)
    {
        Todo todo = Todo.Create(data.Name);

        TodoContext.TodoItems.Add(todo);

        TodoContext.SaveChanges();

        return todo;
    }

    public List<Todo> GetAll() => TodoContext.TodoItems.ToList();

    public bool Delete(Guid id)
    {
        Todo? todo = TodoContext.TodoItems.FirstOrDefault(x => x.Id == id);

        if(todo is null) return false;

        TodoContext.TodoItems.Remove(todo);

        TodoContext.SaveChanges();

        return true;
    }

    public bool MarkCompleted(Guid id)
    {
        Todo? todo = TodoContext.TodoItems.FirstOrDefault(x => x.Id == id);

        if(todo is null) return false;

        todo.MarkCompleted();

        TodoContext.SaveChanges();

        return true;
    }
}