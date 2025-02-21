using AzureTutorials.AppService.DataLayer;

namespace AzureTutorials.AppService.Dtos
{

    public class TodoDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public PriorityTypes PriorityId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}