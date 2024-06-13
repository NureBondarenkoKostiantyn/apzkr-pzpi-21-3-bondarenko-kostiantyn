namespace TrainSmart.Common.DTOs.Auth;

public record LoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
};