using Cytidel.Core.Entities;
using Cytidel.Core.Repositories;
using Cytidel.Infrastructure.Mongo.Documents;
using MongoDB.Driver;
using Omatka.Persistence.MongoDB;

namespace Cytidel.Infrastructure.Mongo.Repositories;
//inject repository
internal class TaskRepository(IMongoRepository<ToDoTaskDocument, Guid> repository) : ITaskRepository
{
    //initialisate the instance repository
    private readonly IMongoRepository<ToDoTaskDocument, Guid> _repository = repository;

    //Add task to Database
    public async Task CreateTaskAsync(ToDoTask task)
        => await _repository.AddAsync(task.AsDocument());

    //Delete task to Database
    public async Task DeleteTaskAsync(Guid id)
        => await _repository.DeleteAsync(id);

    //Get task by id from the database
    public async Task<ToDoTask?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _repository.GetAsync(id, cancellationToken);
        //Convert object from document to entity and return.
        return task?.AsEntity();
    }

    //Get all tasks from the database
    public async Task<IEnumerable<ToDoTask>> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        var task = await _repository.Collection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
        //Convert list of documents from document to entity and return.
        return task.Select(x => x.AsEntity());
    }
    //Update task
    public async Task UpdateTaskAsync(ToDoTask task)
        => await _repository.UpdateAsync(task.AsDocument());
}
