using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands;

[Contract]
public class DeleteTask(Guid id) : ICommand
{
    public Guid Id { get; } = id;
}
