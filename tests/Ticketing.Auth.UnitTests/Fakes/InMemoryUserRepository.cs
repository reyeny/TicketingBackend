using Ticketing.Auth.Application.Interface.Persistence;
using Ticketing.Auth.Application.Dto;

namespace Ticketing.Auth.UnitTests.Fakes;

public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly List<UserDto> _users = [];

    public Task<UserDto?> FindByEmailAsync(string email, CancellationToken ct)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }

    public Task AddAsync(UserCreateDto user, CancellationToken ct)
    {
        _users.Add(new UserDto(user.Id, user.Email, user.PasswordHash));
        return Task.CompletedTask;
    }

    public void Seed(UserDto user) => _users.Add(user);
}