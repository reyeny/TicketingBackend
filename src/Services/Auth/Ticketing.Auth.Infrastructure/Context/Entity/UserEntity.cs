namespace Ticketing.Auth.Infrastructure.Models;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAtUts { get; set; }
}