using Cytidel.Core.Entities;
using Cytidel.Core.Repositories;
using Microsoft.Extensions.Logging;
using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands.Handlers;

internal sealed class CreateTaskHandler(ITaskRepository taskRepository, 
    ILogger<CreateTaskHandler> logger) : ICommandHandler<CreateTask>
{
    private readonly ILogger<CreateTaskHandler> _logger = logger;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task HandleAsync(CreateTask command, CancellationToken cancellationToken = default)
    {
        //create task object
        var task = ToDoTask.Create(command.Description, command.DueTime,
            command.Priority, command.Status, command.Title);
        //trigger a Critical log if is priority is high
        if (command.Priority == TaskPriority.high)
            _logger.LogCritical($"Task with id: {task.Id.Value} and title: {task.Title} has {task.Priority} priority!");
        //adding to the database
        await _taskRepository.CreateTaskAsync(task);
    }
}
