using AutoMapper;
using AzureTutorials.AppService.DataLayer;
using AzureTutorials.AppService.Dtos;
using FluentValidation;

namespace AzureTutorials.AppService.Features
{
    public class EditTodo
    {
        public class Validator : AbstractValidator<TodoModel>
        {
            public Validator()
            {
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PriorityId).IsInEnum();
                RuleFor(x => x.DueDate).GreaterThan(DateTime.Today);
            }
        }

        public class TodoModel
        {
            public required string Title { get; set; }
            public required string Description { get; set; }
            public DateTime DueDate { get; set; }
            public PriorityTypes PriorityId { get; set; }
        }

        public static IResult Handler(TodoDbContext dbContext,
            IMapper mapper, Guid id,
            TodoModel model, Validator validator)
        {
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                var error = new { Error = validationResult.Errors.Single().ErrorMessage };
                return Results.BadRequest(error);
            }

            var todo = dbContext.Todos.SingleOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return Results.NotFound();
            }

            todo.Title = model.Title;
            todo.Description = model.Description;
            todo.DueDate = model.DueDate;
            todo.PriorityId = model.PriorityId;
            todo.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return Results.Ok(mapper.Map<TodoDto>(todo));
        }
    }
}