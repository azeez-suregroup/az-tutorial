using AzureTutorials.AppService.DataLayer;

namespace AzureTutorials.AppService.Features
{
    public class DeleteTodo
    {
        public static IResult Handler(TodoDbContext dbContext, Guid id)
        {
            var todo = dbContext.Todos.SingleOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return Results.NotFound();
            }

            dbContext.Todos.Remove(todo);
            dbContext.SaveChanges();
            return Results.Ok();
        }
    }
}