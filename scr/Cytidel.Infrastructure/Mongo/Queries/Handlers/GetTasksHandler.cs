using Cytidel.Application.Dtos;
using Cytidel.Application.Queries;
using Cytidel.Infrastructure.Mongo.Documents;
using MongoDB.Driver;
using Omatka.CQRS.Queries;

namespace Cytidel.Infrastructure.Mongo.Queries.Handlers;

//inject database and declaring the query and type of return.
internal sealed class GetTasksHandler(IMongoDatabase database) : IQueryHandler<GetTasks, IEnumerable<TaskDto>>
{
    private readonly IMongoDatabase _database = database;
    public async Task<IEnumerable<TaskDto>> HandleAsync(GetTasks query, CancellationToken cancellationToken = default)
    {
        //retrieve all tasks from the colletion
        var collection = await _database.GetCollection<ToDoTaskDocument>("tasks").Find(_ => true).ToListAsync(cancellationToken);
        //convert list of Documents to list of Dtos and returning
        return collection?.Select(x => x.AsDto());
    }
}
