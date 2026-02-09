using FluentAssertions;
using Ticketing.Auth.Application.Dto;
using Ticketing.Auth.Application.UseCases.Register;
using Ticketing.Auth.UnitTests.Fakes;
using Ticketing.Auth.UnitTests.UserBuilder;
using Xunit;

namespace Ticketing.Auth.UnitTests.UseCases.Register;

public sealed class RegisterUserHandleTests
{
    [Fact]
    public async Task Register_Should_Create_User_With_Normalized_Email_And_HashedPassword()
    {
        var repo = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        var handler = new RegisterUserHandle(repo, hasher);

        var res = await handler.Handle(
            new RegisterRequest(TestData.EmailRaw, TestData.Password),
            CancellationToken.None);

        res.UserId.Should().NotBeEmpty();

        var saved = await repo.FindByEmailAsync(TestData.EmailNormalized, CancellationToken.None);
        saved.Should().NotBeNull();
        saved!.Email.Should().Be(TestData.EmailNormalized);
        saved.PasswordHash.Should().Be($"HASH::{TestData.Password}");
    }

    [Fact]
    public async Task Register_Should_Throw_When_Email_Already_Exists()
    {
        var repo = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        repo.Seed(new UserDto(Guid.NewGuid(), TestData.EmailNormalized, "HASH::old"));

        var handler = new RegisterUserHandle(repo, hasher);

        Func<Task> act = async () =>
            await handler.Handle(new RegisterRequest(TestData.EmailRaw, TestData.Password), CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*already*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Register_Should_Throw_When_Email_Is_Empty(string? email)
    {
        var repo = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        var handler = new RegisterUserHandle(repo, hasher);

        Func<Task> act = async () =>
            await handler.Handle(new RegisterRequest(email!, TestData.Password), CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Register_Should_Throw_When_Password_Is_Empty(string? password)
    {
        var repo = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        var handler = new RegisterUserHandle(repo, hasher);

        Func<Task> act = async () =>
            await handler.Handle(new RegisterRequest(TestData.EmailRaw, password!), CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>();
    }
}
