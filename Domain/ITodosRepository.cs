using TodoListApi.Controllers;

namespace TodoListApi.Domain
{
    public interface ITodosRepository
    {
        Task<List<GetTodoItemResponse>> GetAllTodosAsync();
        Task<GetTodoItemResponse> AddTodoItemAsync(PostTodoItemRequest request);
        Task<GetTodoItemResponse> MarkTodoCompletedAsync(GetTodoItemResponse request);
        Task RemoveCompletedTodosAsync();
    }
}