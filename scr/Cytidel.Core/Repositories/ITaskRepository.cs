using Cytidel.Core.Entities;

namespace Cytidel.Core.Repositories;
public interface ITaskRepository
{
    Task<ToDoTask?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ToDoTask>> GetTasksAsync(CancellationToken cancellationToken = default);
    Task CreateTaskAsync(ToDoTask task);
    Task DeleteTaskAsync(Guid id);
    Task UpdateTaskAsync(ToDoTask task);
}
