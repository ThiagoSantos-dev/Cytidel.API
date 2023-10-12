namespace Cytidel.Application.Services;
//interface to hash and validate passwords.
public interface IPasswordService
{
    bool IsValid(string hash, string password);
    string Hash(string password);
}
