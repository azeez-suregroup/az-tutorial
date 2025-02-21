using AutoMapper.QueryableExtensions;
using AzureTutorials.AppService.DataLayer;
using AzureTutorials.AppService.Dtos;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace AzureTutorials.AppService.Features
{
    public class GetTodos
    {
        public static IResult Handler(TodoDbContext dbContext, IConfigurationProvider configuration)
        {
            var t = Environment.GetEnvironmentVariable("Home");
            return Results.Ok(dbContext.Todos.ProjectTo<TodoDto>(configuration));
        }
    }
}