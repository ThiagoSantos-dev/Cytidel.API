using Cytidel.Core.Exceptions;
using System.Text.RegularExpressions;

namespace Cytidel.Core.Entities;

public class User : AggregateRoot
{
    private static readonly Regex EmailRegex = new Regex(
           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }
    public string Username { get; private set; }
    public User(Guid id, string email, string firstname, 
        string lastname, string password, string username, int version = 0)
    {
        if(!IsValidEmail(email)) 
            throw new InvalidEmailException("Empty");

        Id = id;
        Email = email;
        FirstName = firstname; 
        LastName = lastname;
        Password = password;
        Username = username;
        Version = version;
    }
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        
        if (!EmailRegex.IsMatch(email))
            return false;

        return true;
    }
    public static User Create(string email, string firstname,
        string lastname, string password, string username)
        => new(Guid.NewGuid(), email, firstname, lastname, 
            password, username);

    public static User Update(User user, string? firstname = null,
        string? lastname = null, string? username = null)
    {
        var userUpdated = new User(user.Id, user.Email, firstname ?? user.FirstName,
            lastname ?? user.LastName, user.Password, username ?? user.Username, user.Version);
        userUpdated.AddEvent();
        return userUpdated;
    }

    public static User ChangePassword(User user, string password)
    {
       var userUpdated =  new User(user.Id, user.Email, user.FirstName, user.LastName,
            password, user.Username, user.Version);
        userUpdated.AddEvent();
        return userUpdated;
    }
}
