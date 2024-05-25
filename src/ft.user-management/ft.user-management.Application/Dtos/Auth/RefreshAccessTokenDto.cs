namespace ft.user_management.Application.Dtos.Auth;

public class RefreshAccessTokenDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}