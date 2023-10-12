namespace Cytidel.Application.Hubs;
//Hub interface to enable the method inside of the hub
public interface ITasksHub
{
    Task<bool> TasksUpdated();
}
