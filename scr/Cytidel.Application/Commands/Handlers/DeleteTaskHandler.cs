using Cytidel.Application.Exceptions;
using Cytidel.Core.Repositories;
using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands.Handlers;

internal sealed class DeleteTaskHandler(ITaskRepository taskRepository) : ICommandHandler<DeleteTask>
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    public async Task HandleAsync(DeleteTask command, CancellationToken cancellationToken = default)
    {
        //check if exists on the database
        var task = await _taskRepository.GetTaskByIdAsync(command.Id, cancellationToken)
            ?? throw new TaskNotFoundException(command.Id);
        //delete task from the database.
        await _taskRepository.DeleteTaskAsync(command.Id);
    }
}
