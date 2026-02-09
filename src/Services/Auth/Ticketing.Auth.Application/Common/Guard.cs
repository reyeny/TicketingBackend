namespace Ticketing.Auth.Application.Common;

public static class Guard
{
    public static string Email(string? email) 
        => string.IsNullOrWhiteSpace(email) 
            ? throw new ArgumentException("Email is required", nameof(email)) 
            : email.Trim().ToLowerInvariant();
    

    public static void Password(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required", nameof(password));
    }
}