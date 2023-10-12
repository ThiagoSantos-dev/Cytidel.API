namespace Cytidel.Application;
//user context interface on the application
public interface IIdentityContext
{
    Guid Id { get; }
    string Role { get; }
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    IDictionary<string, string> Claims { get; }
}
