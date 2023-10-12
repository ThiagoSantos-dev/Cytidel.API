using Microsoft.AspNetCore.SignalR;

namespace Cytidel.Application.Hubs;
//Hub just to create a connection
public class TasksHub : Hub<ITasksHub>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}
