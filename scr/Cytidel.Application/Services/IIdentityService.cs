using Cytidel.Application.Commands;
using Cytidel.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Cytidel.Application.Services;
//interfaces to create and validate users
public interface IIdentityService
{
    Task<AuthDto> SignInAsync(SignIn command, CancellationToken cancellationToken);
    Task<string> SignUpAsync(SignUp command, CancellationToken cancellationToken);
}
