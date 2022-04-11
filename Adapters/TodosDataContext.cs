using Microsoft.EntityFrameworkCore;

namespace TodoListApi.Adapters;

public class TodosDataContext : DbContext
{

    public TodosDataContext(DbContextOptions<TodosDataContext> options) : base(options)
    {

    }
    public DbSet<TodoItem>? Todos { get; set; } 
}

public class TodoItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool Completed { get; set; } = false;
}
