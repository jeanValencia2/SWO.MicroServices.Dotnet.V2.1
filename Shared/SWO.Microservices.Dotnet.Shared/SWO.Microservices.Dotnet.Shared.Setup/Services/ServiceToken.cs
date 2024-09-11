using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Secrets;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SWO.Microservices.Dotnet.Shared.Setup.Services;

public interface IServiceToken
{
    Task<(string accessToken, DateTime expireIn)> GenerateAccessToken(IdentityUser user, List<string> roles);
    Task<(string refreshToken, DateTime refreshTokenExpiryTime)> GenerateRefreshToken(dynamic user);
    Task<TokenValidationParameters> GetTokenValidationParameters();
    Task<bool> ValidateToken(string jwtToken);
}

public class ServiceToken : IServiceToken
{
    private readonly ISecretManager _secretManager;

    public ServiceToken(ISecretManager secretManager)
    {
        _secretManager = secretManager;
    }

    public async Task<(string accessToken, DateTime expireIn)> GenerateAccessToken(IdentityUser user, List<string> roles)
    {
        var tokenSecrets = await _secretManager.Get<TokenSecrets>("tokensecrets");

        var utcNow = DateTime.UtcNow;
        long iat = new DateTimeOffset(utcNow).ToUnixTimeSeconds();
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, iat.ToString()),
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
        claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecrets.Key!));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expireIn = utcNow.AddSeconds(Convert.ToInt32(tokenSecrets.Lifetime));

        var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claimsIdentity.Claims,
                notBefore: utcNow,
                expires: expireIn,
                audience: tokenSecrets.Audience,
                issuer: tokenSecrets.Issuer
                );

        return (new JwtSecurityTokenHandler().WriteToken(jwt), expireIn);
    }

    public async Task<(string refreshToken, DateTime refreshTokenExpiryTime)> GenerateRefreshToken(dynamic user)
    {
        var tokenSecrets = await _secretManager.Get<TokenSecrets>("tokensecrets");
        var utcNow = DateTime.Now;
        var refreshToken = Guid.NewGuid().ToString();

        return (refreshToken, utcNow.AddSeconds(Convert.ToInt32(tokenSecrets.RefreshTokenExpiryTime)));
    }

    public async Task<TokenValidationParameters> GetTokenValidationParameters()
    {
        var tokenSecrets = await _secretManager.Get<TokenSecrets>("tokensecrets");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenSecrets.Issuer,
            ValidAudience = tokenSecrets.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecrets.Key!)),
            ClockSkew = TimeSpan.Zero
        };

        return tokenValidationParameters;
    }

    public async Task<bool> ValidateToken(string jwtToken)    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = await GetTokenValidationParameters();

        try
        {
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}
