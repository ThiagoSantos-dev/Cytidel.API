using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands
{
    [Contract]
    public class SignUp(string email, string firstname, string lastname, 
        string password, string username) : ICommand
    {
        public string Email { get; } = email;
        public string Password { get; } = password;
        public string FirstName { get; } = firstname;
        public string LastName { get; } = lastname;
        public string Username { get; } = username;
    }
}
