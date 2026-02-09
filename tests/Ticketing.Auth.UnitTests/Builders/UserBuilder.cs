using Ticketing.Auth.Application.Dto;

namespace Ticketing.Auth.UnitTests.Builders;

public sealed class UserBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _email = "test@mail.com";
    private string _passwordHash = "HASH::Qwerty123!";
    private DateTime _createdAtUtc = DateTime.UtcNow;

    public static UserBuilder Default() => new();

    public UserBuilder WithId(Guid id) { _id = id; return this; }
    public UserBuilder WithEmail(string email) { _email = email; return this; }
    public UserBuilder WithPasswordHash(string hash) { _passwordHash = hash; return this; }
    public UserBuilder CreatedAt(DateTime utc) { _createdAtUtc = utc; return this; }

    public UserDto BuildDto() => new(_id, _email, _passwordHash);

    public UserCreateDto BuildCreateDto() => new(_id, _email, _passwordHash, _createdAtUtc);
}