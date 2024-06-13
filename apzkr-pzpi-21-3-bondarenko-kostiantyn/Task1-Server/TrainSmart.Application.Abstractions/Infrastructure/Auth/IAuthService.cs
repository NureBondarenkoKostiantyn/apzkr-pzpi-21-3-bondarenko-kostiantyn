using TrainSmart.Common.DTOs.Auth;

namespace TrainSmart.Application.Abstractions.Infrastructure.Auth;

public interface IAuthService
{
    Task<JwtTokenDto> LoginAsync(
        LoginDto loginDto,
        CancellationToken cancellationToken = default);

    Task SignupAsync(
        SignupDto signupDto,
        CancellationToken cancellationToken = default);

    Task ResetPasswordAsync(
        ChangePasswordDto changePasswordDto,
        CancellationToken cancellationToken = default);
}