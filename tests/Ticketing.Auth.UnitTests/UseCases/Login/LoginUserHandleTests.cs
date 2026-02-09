using FluentAssertions;
using Ticketing.Auth.Application.Dto;
using Ticketing.Auth.Application.UseCases.Login;
using Ticketing.Auth.UnitTests.Fakes;
using Ticketing.Auth.UnitTests.UserBuilder;
using Xunit;

namespace Ticketing.Auth.UnitTests.UseCases.Login;

public sealed class LoginUserHandleTests
{
    [Fact]
    public async Task Login_Should_Return_Token_When_Credentials_Valid()
    {
        var repo = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        var tokens = new FakeTokenService();

        var userId = Guid.NewGuid();
        repo.Seed(new UserDto(userId, TestData.EmailNormalized, $"HASH::{TestData.Password}"));

        var handler = new LoginUserHandle(repo, hasher, tokens);

        var res = await handler.Handle(
            new LoginRequest(TestData.EmailRaw, TestData.Password),
            CancellationToken.None);

        res.AccessToken.Should().Be($"TOKEN::{userId}::{TestData.EmailNormalized}");
    }

    [Fact]
    public async Task Login_Should_Throw_InvalidCredentials_When_User_Not_Found()
    {
        var handler = new LoginUserHandle(
            new InMemoryUserRepository(),
            new FakePasswordHasher(),
            new FakeTokenService());

        Func<Task> act = async () =>
            await handler.Handle(new LoginRequest(TestData.EmailRaw, TestData.Password), CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*Invalid credentials*");
    }

    [Fact]
    public async Task Login_Should_Throw_InvalidCredentials_When_Password_Wrong()
    {
        var repo = new InMemoryUserRepository();
        repo.Seed(new UserDto(Guid.NewGuid(), TestData.EmailNormalized, "HASH::RightPass"));

        var handler = new LoginUserHandle(repo, new FakePasswordHasher(), new FakeTokenService());

        Func<Task> act = async () =>
            await handler.Handle(new LoginRequest(TestData.EmailRaw, "WrongPass"), CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*Invalid credentials*");
    }
}
