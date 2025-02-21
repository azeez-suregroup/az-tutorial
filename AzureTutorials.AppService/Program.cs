using AzureTutorials.AppService.DataLayer;
using AzureTutorials.AppService.Features;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AzureTutorials.AppService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });


            builder.Services.AddDbContext<TodoDbContext>(option =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                option.UseSqlServer(connectionString);
            });
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddValidatorsFromAssemblyContaining<AddTodo.Validator>();
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Services.GetRequiredService<IServiceScopeFactory>()
                .CreateScope().ServiceProvider.GetRequiredService<TodoDbContext>()
                .Database.Migrate();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var todoRoot = app.MapGroup("api/todos")
                  .WithTags("Todos")
                  .WithOpenApi();

            todoRoot.MapGet("", GetTodos.Handler);

            todoRoot.MapGet("{id:guid}", GetTodoById.Handler);

            todoRoot.MapPost("", AddTodo.Handler);

            todoRoot.MapDelete("{id:guid}", DeleteTodo.Handler);

            todoRoot.MapPut("{id:guid}", EditTodo.Handler);

            app.Run();
        }


    }
}
