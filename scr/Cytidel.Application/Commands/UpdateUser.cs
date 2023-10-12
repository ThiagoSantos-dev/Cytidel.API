using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands;

[Contract]
public class UpdateUser(string email, string? firstname,
    string? lastname, string? username) : ICommand
{
    public string Email { get; } = email;
    public string? FirstName { get; } = firstname;
    public string? LastName { get; } = lastname;
    public string? Username { get; } = username;
}
