using Cytidel.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace Cytidel.Infrastructure.Auth;
//Hash and compare passwords service
public class PasswordService(IPasswordHasher<IPasswordService> passwordHasher) : IPasswordService
{
    private readonly IPasswordHasher<IPasswordService> _passwordHasher = passwordHasher;

    public string Hash(string password) => _passwordHasher.HashPassword(this, password);

    public bool IsValid(string hash, string password)
        => _passwordHasher.VerifyHashedPassword(this, hash, password) != PasswordVerificationResult.Failed;
}
