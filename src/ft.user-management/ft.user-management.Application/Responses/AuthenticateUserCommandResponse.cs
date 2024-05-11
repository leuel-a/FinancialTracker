namespace ft.user_management.Application.Responses;

public class AuthenticateUserCommandResponse : BaseCommandResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}