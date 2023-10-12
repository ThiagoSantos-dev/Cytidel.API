using Cytidel.Application.Commands;
using Cytidel.Application.Services;
using Cytidel.Core.Exceptions;
using Cytidel.Core.Repositories;
using Cytidel.Infrastructure.Exceptions;
using Cytidel.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace Cytidel.Tests.Unit.Application.Services;

public class IdentityServiceTests
{
    //initiate a specific method to be test
    private Task ActSignIn(SignIn command, CancellationToken cancellation)
        => _identity.SignInAsync(command,cancellation);
    private Task ActSignUp(SignUp command, CancellationToken cancellation) 
        => _identity.SignUpAsync(command, cancellation);

    //test sign in with empty email
    [Fact]
    public async Task act_sign_in_should_throw_email_exception_current_email_is_empty()
    {
        var email = "";
        var password = "XXXX";
        var command = new SignIn(email, password);
        var cancellationToken = new CancellationToken();
        var exception = await Record.ExceptionAsync(async () => await ActSignIn(command,cancellationToken));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidCredentialsException>();
    }
    //test sign in with invalid email
    [Fact]
    public async Task act_sign_in_should_throw_email_exception_current_email_is_not_valid()
    {
        var email = "test@test";
        var password = "XXXX";
        var command = new SignIn(email, password);
        var cancellationToken = new CancellationToken();
        var exception = await Record.ExceptionAsync(async () => await ActSignIn(command,cancellationToken));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidEmailException>();
    }
    //test sign up with empty email
    [Fact]
    public async Task act_sign_up_should_throw_email_exception_current_email_is_empty()
    {
        var email = "";
        var password = "XXXX";
        var firstname = "test";
        var lastname = "test last";
        var username = "test username";
        var cancellationToken = new CancellationToken();
        var command = new SignUp(email,firstname,lastname, password, username);
        var exception = await Record.ExceptionAsync(async () => await ActSignUp(command, cancellationToken));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidCredentialsException>();
    }
    [Fact]
    //test sign in with invalid email
    public async Task act_sign_up_should_throw_email_exception_current_email_is_not_valid()
    {
        var email = "test@test";
        var password = "XXXX";
        var firstname = "test";
        var lastname = "test last";
        var username = "test username";
        var cancellationToken = new CancellationToken();
        var command = new SignUp(email, firstname, lastname, password, username);
        var exception = await Record.ExceptionAsync(async () => await ActSignUp(command, cancellationToken));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidEmailException>();
    }
    #region Arrange
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<IdentityService> _logger;
    private readonly IdentityService _identity;
    public IdentityServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordService = Substitute.For<IPasswordService>();
        _jwtProvider = Substitute.For<IJwtProvider>();
        _logger = Substitute.For<ILogger<IdentityService>>();
        _identity = new IdentityService(_jwtProvider,_passwordService,_userRepository,_logger);
    }
#endregion
}
