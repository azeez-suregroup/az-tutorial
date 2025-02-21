using AutoMapper;
using AzureTutorials.AppService.DataLayer;
using AzureTutorials.AppService.Dtos;

namespace AzureTutorials.AppService.Features
{
    public class GetTodoById
    {
        public static IResult Handler(TodoDbContext dbContext, Guid id, IMapper mapper)
        {
            var todo = dbContext.Todos.SingleOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(mapper.Map<TodoDto>(todo));
        }
    }
}