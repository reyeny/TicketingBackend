using Ticketing.Auth.Application.Interface.Security;

namespace Ticketing.Auth.UnitTests.Fakes;

public sealed class FakePasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) => $"HASH::{password}";
    public bool VerifyHashedPassword(string password, string passwordHash) => passwordHash == $"HASH::{password}";
}