
using ToDoApi.Models;

namespace ToDoApi.Services;

public interface ITodoService 
{
    Todo Create(Todo data);

    List<Todo> GetAll();

    bool Delete(Guid id);

    bool MarkCompleted(Guid id);
}