using Cytidel.Application.Dtos;
using Cytidel.Application.Services;
using Omatka.Auth;

namespace Cytidel.Infrastructure.Auth;
//Generate tokens
public class JwtProvider(IJwtHandler jwtHandler) : IJwtProvider
{
    private readonly IJwtHandler _jwtHandler = jwtHandler;

    public AuthDto Create(Guid userId, string role,
            string audience = null, IDictionary<string, IEnumerable<string>> claims = null)
    {
        var jwt = _jwtHandler.CreateToken(userId.ToString("N"), role, audience, claims);

        return new AuthDto
        {
            AccessToken = jwt.AccessToken,
            Expires = UnixTimeStampToDateTime(jwt.Expires).AddMinutes(60)
        };
    }
    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
}
