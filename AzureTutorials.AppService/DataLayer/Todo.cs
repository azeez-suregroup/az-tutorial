using System.ComponentModel.DataAnnotations;

namespace AzureTutorials.AppService.DataLayer
{
    public class Todo
    {


        [Key] public Guid Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public PriorityTypes PriorityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}