using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TrainSmart.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TrainSmart.Application.Abstractions.Infrastructure.Auth;
using TrainSmart.Common.Options;
using TrainSmart.Common.Options.Jwt;
using TrainSmart.Domain.AggregateRoots;

namespace TrainSmart.Infrastructure.Auth.Services;

public class JwtService: IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<User> _userManager;
    
    public JwtService(
        IOptions<JwtOptions> jwtSettings,
        UserManager<User> userManager)
    {
        _jwtOptions = jwtSettings.Value;
        _userManager = userManager;
    }
    
    public SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    
    public async Task<List<Claim>> GetClaimsAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            throw new ApplicationException("User was not found");
        }
    
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(ClaimTypes.Role, "User")
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        claims.AddRange(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

        return claims;
    }
    
    public JwtSecurityToken GenerateToken(SigningCredentials signingCredentials,
        IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: signingCredentials);

        return token;
    }
}