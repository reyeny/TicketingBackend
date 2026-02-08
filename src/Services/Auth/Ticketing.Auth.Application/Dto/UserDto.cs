namespace Ticketing.Auth.Application.Dto;

public sealed record UserDto(Guid Id, string Email, string PasswordHash);
