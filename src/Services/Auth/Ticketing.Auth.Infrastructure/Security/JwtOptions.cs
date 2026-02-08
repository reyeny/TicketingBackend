namespace Ticketing.Auth.Infrastructure.Security;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string SigningKey { get; set; } = null!;
    public int ExpiresMinutes { get; set; } = 60;
}