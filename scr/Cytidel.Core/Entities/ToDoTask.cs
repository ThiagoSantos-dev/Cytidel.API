using Cytidel.Core.Exceptions;

namespace Cytidel.Core.Entities;

public class ToDoTask : AggregateRoot
{
    public DateTime CreatedAt { get; private set; }
    public string? Description { get; private set; }
    public DateTime DueDate { get; private set; } 
    public TaskPriority Priority { get; private set; }
    public TaskStatus Status { get; private set; }
    public string Title { get; private set; }
    public ToDoTask(Guid id, DateTime createdAt, string? description, 
        DateTime dueDate, TaskPriority priority, TaskStatus status, 
        string title, int version = 0)
    {
        if (!IsValidTitle(title))
            throw new InvalidTitleException();
        
        Id = id;
        CreatedAt = createdAt;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Status = status;
        Title = title;
        Version = version;
    }
    public static bool IsValidTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return false;

        return true;
    }
    public static ToDoTask Create(string? description,
        DateTime dueDate, TaskPriority priority, TaskStatus status,
        string title)
        => new(Guid.NewGuid(), DateTime.UtcNow, description, dueDate, priority, status, title);

    public static ToDoTask Update(ToDoTask task, string? title = null, string? description = null, 
        DateTime? dueTime = null, TaskPriority? priority = null, TaskStatus? status = null)
    {
        var updatedTask = new ToDoTask(task.Id, task.CreatedAt, description ?? task.Description, 
            dueTime ?? task.DueDate, priority ?? task.Priority, status ?? task.Status, 
            title ?? task.Title, task.Version);
        updatedTask.AddEvent();
        return updatedTask;
    }
}
