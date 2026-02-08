using Microsoft.EntityFrameworkCore;
using Ticketing.Auth.Application.Dto;
using Ticketing.Auth.Application.Interface.Persistence;
using Ticketing.Auth.Infrastructure.Context;
using Ticketing.Auth.Infrastructure.Models;

namespace Ticketing.Auth.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AuthDbContext db) : IUserRepository
{
    public async Task<UserDto?> FindByEmailAsync(string email, CancellationToken ct)
    {
        var user = await  db.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, ct);
        return user is null ? null : new UserDto(user.Id,  user.Email,  user.PasswordHash);
    }

    public async Task AddAsync(UserCreateDto user, CancellationToken ct)
    {
        db.Users.Add(new UserEntity
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            CreatedAtUts = user.CreatedAtUtc
        });
        await db.SaveChangesAsync(ct);
    }
}