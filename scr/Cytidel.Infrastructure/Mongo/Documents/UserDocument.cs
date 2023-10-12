using Omatka.Types;

namespace Cytidel.Infrastructure.Mongo.Documents;

public class UserDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public int Version { get; set; }
}
