using FluentAssertions;
using Ticketing.Auth.Infrastructure.Security;
using Xunit;

namespace Ticketing.Auth.UnitTests.Security;

public sealed class PasswordHasherTests
{
    [Fact]
    public void HashPassword_Should_Return_NonEmptyHash()
    {
        var hasher = new PasswordHasher();

        var hash = hasher.HashPassword("Qwerty123!");

        hash.Should().NotBeNullOrWhiteSpace();
        hash.Should().NotBe("Qwerty123!");
    }

    [Fact]
    public void VerifyPassword_Should_Return_True_For_CorrectPassword()
    {
        var hasher = new PasswordHasher();

        var hash = hasher.HashPassword("Qwerty123!");

        hasher.VerifyHashedPassword("Qwerty123!", hash).Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_Should_Return_False_For_WrongPassword()
    {
        var hasher = new PasswordHasher();

        var hash = hasher.HashPassword("Qwerty123!");

        hasher.VerifyHashedPassword("WrongPass", hash).Should().BeFalse();
    }
}