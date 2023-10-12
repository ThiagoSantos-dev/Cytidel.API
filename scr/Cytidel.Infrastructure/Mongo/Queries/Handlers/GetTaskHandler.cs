using Cytidel.Application.Dtos;
using Cytidel.Application.Queries;
using Cytidel.Infrastructure.Mongo.Documents;
using MongoDB.Driver;
using Omatka.CQRS.Queries;

namespace Cytidel.Infrastructure.Mongo.Queries.Handlers;

//inject database and declaring the query and type of return.
internal sealed class GetTaskHandler(IMongoDatabase database) : IQueryHandler<GetTask, TaskDto>
{
    private readonly IMongoDatabase _database = database;
    public async Task<TaskDto> HandleAsync(GetTask query, CancellationToken cancellationToken = default)
    {
        //retrieve specific task from the colletion
        var document = await _database.GetCollection<ToDoTaskDocument>("tasks")
                                        .Find(t => t.Id == query.TaskId)
                                        .SingleOrDefaultAsync();
        //convert Document to Dto and returning
        return document?.AsDto();
    }
}
