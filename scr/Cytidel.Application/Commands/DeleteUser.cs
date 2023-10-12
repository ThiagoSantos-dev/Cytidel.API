using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands;

[Contract]
public class DeleteUser(Guid userId) : ICommand
{
    public Guid Id { get; } = userId;
}
