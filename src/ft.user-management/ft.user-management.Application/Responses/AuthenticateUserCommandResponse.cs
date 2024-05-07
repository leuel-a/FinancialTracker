namespace ft.user_management.Application.Responses;

public class AuthenticateUserCommandResponse : BaseCommandResponse
{
    public string? Token { get; set; }
}