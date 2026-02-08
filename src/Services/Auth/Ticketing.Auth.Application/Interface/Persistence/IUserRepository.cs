using Ticketing.Auth.Application.Dto;

namespace Ticketing.Auth.Application.Interface.Persistence;

public interface IUserRepository
{
    Task<UserDto?> FindByEmailAsync(string email, CancellationToken ct);
    Task AddAsync(UserCreateDto user, CancellationToken ct);
}