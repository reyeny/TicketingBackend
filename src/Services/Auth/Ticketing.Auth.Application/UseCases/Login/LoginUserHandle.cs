using Ticketing.Auth.Application.Common;
using Ticketing.Auth.Application.Dto;
using Ticketing.Auth.Application.Interface.Persistence;
using Ticketing.Auth.Application.Interface.Security;

namespace Ticketing.Auth.Application.UseCases.Login;

public sealed class LoginUserHandle(IUserRepository users, IPasswordHasher hasher, ITokenService tokens)
{
    public async Task<LoginResponse> Handle(LoginRequest req, CancellationToken ct)
    {
        var email = Guard.Email(req.Email);
        Guard.Password(req.Password);
        
        var user = await users.FindByEmailAsync(email, ct);
        if (user is null || !hasher.VerifyHashedPassword(req.Password, user!.PasswordHash))
            throw new InvalidOperationException("Invalid credentials");

        var token = tokens.CreateAccessToken(user.Id, email);
        return new LoginResponse(token);
    }
}