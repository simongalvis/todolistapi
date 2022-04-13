using Microsoft.AspNetCore.Mvc;
using TodoListApi.Domain;

namespace TodoListApi.Controllers;

public class TodosController : ControllerBase
{
    private readonly ITodosRepository _repository;

    public TodosController(ITodosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("todos")]
    public async Task<ActionResult> GetTodos()
    {
        await Task.Delay(5000);
        var data = await _repository.GetAllTodosAsync();
        return Ok(new {data});
    }

    [HttpPost("todos")]
    public async Task<ActionResult> AddTodo([FromBody] PostTodoItemRequest request)
    {
        await Task.Delay(new Random().Next(3000,4500)); // don't do this!!
        GetTodoItemResponse response = await _repository.AddTodoItemAsync(request);
        return StatusCode(201, response);
    }

    [HttpPut("completed-todos")]
    public async Task<ActionResult> MarkTodoCompleted([FromBody] GetTodoItemResponse request)
    {
         await Task.Delay(new Random().Next(3000,4500)); // don't do this!!
        GetTodoItemResponse response = await _repository.MarkTodoCompletedAsync(request);
        return Accepted(response);
    }

    [HttpDelete("completed-todos")]
    public async Task<ActionResult> RemoveCompletedTodos()
    {
        await _repository.RemoveCompletedTodosAsync();
        return Accepted();
    }
}

public record GetTodoItemResponse(string id, string description, bool completed);

public record PostTodoItemRequest(string description);