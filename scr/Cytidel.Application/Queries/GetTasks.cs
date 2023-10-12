using Cytidel.Application.Dtos;
using Omatka.CQRS.Queries;

namespace Cytidel.Application.Queries;
//request all the tasks
public class GetTasks : IQuery<IEnumerable<TaskDto>>
{
}
