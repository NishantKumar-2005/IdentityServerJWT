namespace Squib.UserService.API.Model;

public record class UserDto
{
    public int Id { get; init; }
    public string? Email { get; init; }
    public string? FirstName { get; init; }

}
