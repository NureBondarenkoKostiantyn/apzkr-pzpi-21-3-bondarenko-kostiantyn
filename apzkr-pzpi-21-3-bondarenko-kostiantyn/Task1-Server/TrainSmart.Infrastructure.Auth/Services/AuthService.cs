using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TrainSmart.Application.Abstractions.Infrastructure.Auth;
using TrainSmart.Common.DTOs.Auth;
using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Infrastructure.Auth.Services;

public class AuthService: IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtHandler;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper, 
        IJwtService jwtHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

    public async Task<JwtTokenDto> LoginAsync(
        LoginDto loginDto,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email)
            ?? throw new ApplicationException("Invalid email or password");

        var result = await _signInManager
            .PasswordSignInAsync(user, loginDto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Invalid email or password");
        }

        var claims = await _jwtHandler.GetClaimsAsync(user.Id);
        var signingCredentials = _jwtHandler.GetSigningCredentials();
        var token = _jwtHandler.GenerateToken(signingCredentials, claims);

        return new JwtTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }

    public async Task SignupAsync(
        SignupDto signupDto,
        CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(signupDto);

        user.UserName = user.Email;
        
        var result = await _userManager.CreateAsync(user, signupDto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException(result.ToString());
        }
    }

    public async Task ResetPasswordAsync(
        ChangePasswordDto changePasswordDto,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(changePasswordDto.Email)
                   ?? throw new ApplicationException($"User with email {changePasswordDto.Email} was not found");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, changePasswordDto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Failed to reset password");
        }
    }
}