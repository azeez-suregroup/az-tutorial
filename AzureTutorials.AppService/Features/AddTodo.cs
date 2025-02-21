using AutoMapper;
using AzureTutorials.AppService.DataLayer;
using AzureTutorials.AppService.Dtos;
using FluentValidation;

namespace AzureTutorials.AppService.Features
{
    public class AddTodo
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
            IMapper mapper,
            TodoModel model, Validator validator)
        {
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                var error = new { Error = validationResult.Errors.Single().ErrorMessage };
                return Results.BadRequest(error);
            }


            var todo = new Todo()
            {
                DueDate = model.DueDate,
                PriorityId = model.PriorityId,
                Description = model.Description,
                Title = model.Title,
                CreatedAt = DateTime.Now
            };
            dbContext.Todos.Add(todo);
            dbContext.SaveChanges();
            return Results.Ok(mapper.Map<TodoDto>(todo));
        }
    }
}