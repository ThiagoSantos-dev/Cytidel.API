using Cytidel.Application.Exceptions;
using Cytidel.Core.Entities;
using Cytidel.Core.Repositories;
using Microsoft.Extensions.Logging;
using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands.Handlers;

internal sealed class EditTaskHandler(ITaskRepository taskRepository, 
    ILogger<CreateTaskHandler> logger) : ICommandHandler<EditTask>
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly ILogger<CreateTaskHandler> _logger = logger;
    public async Task HandleAsync(EditTask command, CancellationToken cancellationToken = default)
    {
        //check if exists on the database.
        var task = await _taskRepository.GetTaskByIdAsync(command.Id, cancellationToken)
        ?? throw new TaskNotFoundException(command.Id);

        //create a new object to update.
        var updatedTask = ToDoTask.Update(task, command.Title, command.Description, 
            command.DueTime, command.Priority, command.Status);
        //trigger a Critical log if is priority is high.
        if (command.Priority == TaskPriority.high)
            _logger.LogCritical($"Task with id: {updatedTask.Id.Value} and title: {updatedTask.Title} has been updated and is {updatedTask.Priority} priority!");
        //update the task on the database.
        await _taskRepository.UpdateTaskAsync(updatedTask);
    }
}
