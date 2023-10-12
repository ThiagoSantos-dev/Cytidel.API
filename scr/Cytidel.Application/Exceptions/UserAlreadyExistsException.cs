namespace Cytidel.Application.Exceptions;

public class UserAlreadyExistsException(string email) : AppException($"User with email: {email} already exists.")
{
    public override string Code { get; } = "user_already_exists";
    public string Email { get; } = email;
}
