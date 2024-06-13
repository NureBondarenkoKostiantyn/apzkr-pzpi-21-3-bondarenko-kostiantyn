using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TrainSmart.Application.Abstractions.Infrastructure.Auth;

public interface IJwtService
{
    SigningCredentials GetSigningCredentials();
    Task<List<Claim>> GetClaimsAsync(Guid userId);
    JwtSecurityToken GenerateToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims);
}