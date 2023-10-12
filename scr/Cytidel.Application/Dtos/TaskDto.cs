using Cytidel.Core.Entities;
using TaskStatus = Cytidel.Core.Entities.TaskStatus;

namespace Cytidel.Application.Dtos
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public string Title { get; set; }
    }
}
