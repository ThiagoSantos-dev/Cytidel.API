using Cytidel.Application.Dtos;
using Omatka.CQRS.Queries;

namespace Cytidel.Application.Queries;
//request a single task details
public class GetTask : IQuery<TaskDto>
{
    public Guid TaskId { get; set; }
}
