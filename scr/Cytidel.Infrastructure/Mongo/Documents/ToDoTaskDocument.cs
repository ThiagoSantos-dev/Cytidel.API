using Cytidel.Core.Entities;
using Omatka.Types;

namespace Cytidel.Infrastructure.Mongo.Documents;

public class ToDoTaskDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public long CreatedAt { get; set; }
    public string? Description { get; set; }
    public long DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public Core.Entities.TaskStatus Status { get; set; }
    public string Title { get; set; }
    public int Version { get; set; }
}
