using Microsoft.EntityFrameworkCore;
using TodoListApi.Adapters;
using TodoListApi.Controllers;

namespace TodoListApi.Domain;

public class TodosRepository : ITodosRepository
{

    private readonly TodosDataContext _context;

    public TodosRepository(TodosDataContext context)
    {
        _context = context;
    }

    public async Task<GetTodoItemResponse> AddTodoItemAsync(PostTodoItemRequest request)
    {
        var todo = new TodoItem { Description = request.description, Completed = false };
        _context.Todos!.Add(todo);
        await _context.SaveChangesAsync();
        return new GetTodoItemResponse(todo.Id.ToString(), todo.Description, todo.Completed);
    }

    public async Task<List<GetTodoItemResponse>> GetAllTodosAsync()
    {
        return await _context.Todos!.Select(t => new GetTodoItemResponse(t.Id.ToString(), t.Description, t.Completed)).ToListAsync();
    }

    public async Task<GetTodoItemResponse> MarkTodoCompletedAsync(GetTodoItemResponse request)
    {
        var id = int.Parse(request.id);
        var todo = await _context.Todos!.Where(t => t.Id == id).SingleAsync();
        todo.Completed = true;
        await _context.SaveChangesAsync();
        var response = request with { completed = true };
        return response;

    }

    public async Task RemoveCompletedTodosAsync()
    {
        var completedTodos = await _context.Todos!.Where(t => t.Completed == true).ToListAsync();
        var ids = new List<int>();
        foreach(var todo in completedTodos)
        {
            _context.Todos!.Remove(todo);
        }
        await _context.SaveChangesAsync();

       
    }
}
