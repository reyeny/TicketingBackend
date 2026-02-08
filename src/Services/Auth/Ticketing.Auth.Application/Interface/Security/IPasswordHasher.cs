namespace Ticketing.Auth.Application.Interface.Security;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string password, string passwordHash);
}