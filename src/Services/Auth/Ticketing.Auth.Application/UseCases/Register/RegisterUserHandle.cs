using Ticketing.Auth.Application.Common;
using Ticketing.Auth.Application.Dto;
using Ticketing.Auth.Application.Interface.Persistence;
using Ticketing.Auth.Application.Interface.Security;

namespace Ticketing.Auth.Application.UseCases.Register;

public sealed class RegisterUserHandle(IUserRepository user, IPasswordHasher hasher)
{
    public async Task<RegisterResponse> Handle(RegisterRequest req, CancellationToken ct)
    {
        var email = Guard.Email(req.Email);
        Guard.Password(req.Password);
        
        var exists = await user.FindByEmailAsync(email, ct);
        if (exists is not null)
            throw new InvalidOperationException("User Already Exists");
        
        var userId = Guid.NewGuid();
        var hash = hasher.HashPassword(req.Password);

        await user.AddAsync(new UserCreateDto(userId, email, hash, DateTime.UtcNow), ct);
        return new RegisterResponse(userId);
    }
}