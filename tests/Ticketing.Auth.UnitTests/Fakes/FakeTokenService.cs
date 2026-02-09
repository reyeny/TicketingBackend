using Ticketing.Auth.Application.Interface.Security;

namespace Ticketing.Auth.UnitTests.Fakes;

public sealed class FakeTokenService : ITokenService
{
    public string CreateAccessToken(Guid userId, string email) => $"TOKEN::{userId}::{email}";
}