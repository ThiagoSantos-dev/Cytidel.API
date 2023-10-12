using Cytidel.Application.Dtos;

namespace Cytidel.Application.Services;
//interface to create JWT token
public interface IJwtProvider
{
    AuthDto Create(Guid userId, string role,
            string audience = null, IDictionary<string, IEnumerable<string>> claims = null);
}
