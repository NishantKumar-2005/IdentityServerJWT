namespace Squib.IdentityServer.Models;

public record class LoginModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
