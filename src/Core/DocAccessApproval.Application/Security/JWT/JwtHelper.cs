using DocAccessApproval.Application.Extensions;
using DocAccessApproval.Application.Security.Encryption;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DocAccessApproval.Application.Security.JWT;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, IEnumerable<Role> roles)
    {
        _accessTokenExpiration = DateTime.UtcNow.AddHours(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
                                                   SigningCredentials signingCredentials,
                                                   IEnumerable<Role> roles)
    {
        JwtSecurityToken jwt = new(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: SetClaims(user, roles),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, IEnumerable<Role> roles)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());

        claims.AddEmail(user.Email);

        claims.AddUserFirstName(user.FirstName.Trim());

        claims.AddUserSurname(user.LastName?.Trim());

        claims.AddRoles(roles.Select(c => c.Name).ToArray());
        return claims;
    }
}