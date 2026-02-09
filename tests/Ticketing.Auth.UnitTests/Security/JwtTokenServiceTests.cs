using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Ticketing.Auth.Infrastructure.Security;
using Ticketing.Auth.UnitTests.UserBuilder;
using Xunit;

namespace Ticketing.Auth.UnitTests.Security;

public sealed class JwtTokenServiceTests
{
    [Fact]
    public void CreateAccessToken_Should_Contain_Sub_And_Email_And_Metadata()
    {
        var opt = new JwtOptions
        {
            Issuer = "ticketing-auth",
            Audience = "ticketing-clients",
            SigningKey = "AiuySt7323KKnBFshp0/NZWux4aeXaY2c46T6niwu3E=",
            ExpiresMinutes = 60
        };

        var svc = new JwtTokenService(Options.Create(opt));

        var userId = Guid.NewGuid();
        const string email = "test@mail.com";

        var token = svc.CreateAccessToken(userId, email);

        var jwt = AssertEx.ReadJwt(token);

        jwt.Issuer.Should().Be(opt.Issuer);
        AssertEx.HasAudience(jwt, opt.Audience);

        AssertEx.ClaimEquals(jwt, JwtRegisteredClaimNames.Sub, userId.ToString());
        AssertEx.ClaimEquals(jwt, JwtRegisteredClaimNames.Email, email);

        jwt.ValidTo.Should().BeAfter(DateTime.UtcNow);
    }
}