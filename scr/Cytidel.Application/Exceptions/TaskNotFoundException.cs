namespace Cytidel.Application.Exceptions;

public class TaskNotFoundException(Guid taskId)
    : AppException($"Task with TaskId: {taskId} not found.")
{
    public override string Code { get; } = "task_not_found";
    public Guid Id { get; } = taskId;
}
