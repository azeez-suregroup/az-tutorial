using Microsoft.EntityFrameworkCore;

namespace AzureTutorials.AppService.DataLayer
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }
    }
}