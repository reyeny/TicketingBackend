namespace Ticketing.Auth.Application.Interface.Security;

public interface ITokenService
{
    string CreateAccessToken(Guid userId, string email);
}