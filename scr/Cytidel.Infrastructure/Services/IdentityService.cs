using Cytidel.Application.Commands;
using Cytidel.Application.Dtos;
using Cytidel.Application.Services;
using Cytidel.Core.Entities;
using Cytidel.Core.Exceptions;
using Cytidel.Core.Repositories;
using Cytidel.Infrastructure.Auth;
using Cytidel.Infrastructure.Exceptions;
using Cytidel.Infrastructure.Mongo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;
using System.Text.RegularExpressions;

namespace Cytidel.Infrastructure.Services
{
    //Inject the Services
    public class IdentityService(IJwtProvider jwtProvider,
        IPasswordService passwordService, IUserRepository userRepository, 
        ILogger<IdentityService> logger) : IIdentityService
    {
        //Regex to verify emails.
        private static readonly Regex EmailRegex = new Regex(
           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //initializate services.
        private readonly ILogger<IdentityService> _logger = logger;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        //Sign In Check credentials
        public async Task<AuthDto> SignInAsync(SignIn command, CancellationToken cancellationToken)
        {
            //verify if the email is not empty or null
            if (string.IsNullOrWhiteSpace(command.Email))
            {
                _logger.LogError($"Invalid credentials");
                throw new InvalidCredentialsException();
            }
            //verify if is a valid email format
            if (!EmailRegex.IsMatch(command.Email))
            {
                _logger.LogError($"Invalid email: {command.Email}");
                throw new InvalidEmailException(command.Email);
            }
            //retrieve user from the database
            var user = await _userRepository.GetUserByEmailAsync(command.Email, cancellationToken);
            //if the user is null we do not have on the database so return not found.
            if (user is null)
            {
                _logger.LogError($"User with email: {command.Email} was not found.");
                return new AuthDto() { Status = "not found"};
            }
            //check if the password matches 
            if (!_passwordService.IsValid(user.Password, command.Password))
            {
                _logger.LogError($"Password do not match User with email: {command.Email}.");
                return new AuthDto() { Status = "password" };
            }
            //generate the Auth token and expire time
            var auth = _jwtProvider.Create(user.Id, "user");
            //return auth to controller
            return auth;
        }

        public async Task<string> SignUpAsync(SignUp command, CancellationToken cancellationToken)
        {
            //verify if the email is not empty or null
            if (string.IsNullOrWhiteSpace(command.Email))
            {
                _logger.LogError($"Invalid credentials");
                throw new InvalidCredentialsException();
            }
            //verify if is a valid email format
            if (!EmailRegex.IsMatch(command.Email))
            {
                _logger.LogError($"Invalid email: {command.Email}");
                throw new InvalidEmailException(command.Email);
            }
            //retrieve user from the database
            var user = await _userRepository.GetUserByEmailAsync(command.Email, cancellationToken);
            //if the user is not null we do have on the database so return conflict.
            if (user is not null) 
            {
                return "conflict";
            }
            //hash the password to store on the database
            var password = _passwordService.Hash(command.Password);
            //create a object user
            var userCreated = User.Create(command.Email, command.FirstName,
                command.LastName, password, command.Username);
            //add user on the database
            await _userRepository.AddAsync(userCreated);
            //return accepted response to controller
            return "accepted";
        }
    }
}
