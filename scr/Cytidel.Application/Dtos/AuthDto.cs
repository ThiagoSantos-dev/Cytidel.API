namespace Cytidel.Application.Dtos;

public class AuthDto
{
    public string AccessToken { get; set; }
    public DateTime Expires { get; set; }
    public string Status { get; set; }
}
