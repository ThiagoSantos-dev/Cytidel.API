using Cytidel.Core.Entities;
using Omatka.CQRS.Commands;
using TaskStatus = Cytidel.Core.Entities.TaskStatus;

namespace Cytidel.Application.Commands;

[Contract]
public class CreateTask(string title, string? description, DateTime dueTime, 
    TaskPriority priority, TaskStatus status) : ICommand
{
    public string? Description { get; } = description;
    public string Title { get; } = title;
    public DateTime DueTime { get; } = dueTime;
    public TaskPriority Priority { get; } = priority;
    public TaskStatus Status { get; } = status;
}
