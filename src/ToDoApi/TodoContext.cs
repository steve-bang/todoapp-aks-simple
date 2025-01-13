
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace TodoApi.Context;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<Todo> TodoItems { get; set; }
}