namespace Ticketing.Auth.Application.Dto;

public sealed record UserCreateDto(Guid Id, string Email, string PasswordHash, DateTime CreatedAtUtc);
