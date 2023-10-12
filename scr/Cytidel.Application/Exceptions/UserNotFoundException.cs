namespace Cytidel.Application.Exceptions;

public class UserNotFoundException : AppException
{
    public override string Code { get; } = "user_not_found";
    public Guid UserId { get; }
    public string Email { get; }
    public UserNotFoundException(Guid userId) : base($"User with Id: {userId} not found.")
    {
        UserId = userId;
    }
    public UserNotFoundException(string email) : base($"User with Email: {email} not found.")
    {
        Email = email;
    }
}
