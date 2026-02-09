using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;

namespace Ticketing.Auth.UnitTests.UserBuilder;

public static class AssertEx
{
    public static JwtSecurityToken ReadJwt(string token)
    {
        token.Should().NotBeNullOrWhiteSpace();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        jwt.Should().NotBeNull();
        return jwt;
    }

    public static void ClaimEquals(JwtSecurityToken jwt, string type, string expected)
    {
        var value = jwt.Claims.FirstOrDefault(c => c.Type == type)?.Value;
        value.Should().Be(expected);
    }

    public static void HasAudience(JwtSecurityToken jwt, string audience)
    {
        jwt.Audiences.Should().Contain(audience);
    }
}