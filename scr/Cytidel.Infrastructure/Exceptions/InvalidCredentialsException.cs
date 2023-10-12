using Cytidel.Application.Exceptions;

namespace Cytidel.Infrastructure.Exceptions;

public class InvalidCredentialsException : AppException
{
    public override string Code { get; } = "invalid_credentials";
    public string Email { get; }
    public InvalidCredentialsException(string email) : base("Invalid credentials.")
    {
        Email = email;
    }
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}
