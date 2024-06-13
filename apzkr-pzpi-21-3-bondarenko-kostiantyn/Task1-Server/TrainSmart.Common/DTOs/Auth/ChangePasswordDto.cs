namespace TrainSmart.Common.DTOs.Auth;

public record ChangePasswordDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
};