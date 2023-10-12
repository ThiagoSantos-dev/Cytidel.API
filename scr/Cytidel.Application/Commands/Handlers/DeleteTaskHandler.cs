using Cytidel.Application.Exceptions;
using Cytidel.Application.Hubs;
using Cytidel.Core.Repositories;
using Microsoft.AspNetCore.SignalR;
using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands.Handlers;

internal sealed class DeleteTaskHandler(ITaskRepository taskRepository,
    IHubContext<TasksHub> notifyUsers) : ICommandHandler<DeleteTask>
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IHubContext<TasksHub> _notifyUsers = notifyUsers;
    public async Task HandleAsync(DeleteTask command, CancellationToken cancellationToken = default)
    {
        //check if exists on the database
        var task = await _taskRepository.GetTaskByIdAsync(command.Id, cancellationToken)
            ?? throw new TaskNotFoundException(command.Id);
        //delete task from the database.
        await _taskRepository.DeleteTaskAsync(command.Id);
        //notify all connected uses
        await _notifyUsers.Clients.All.SendAsync("TasksHasUpdated");
    }
}
