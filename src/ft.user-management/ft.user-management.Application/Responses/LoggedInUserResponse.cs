namespace ft.user_management.Application.Responses;

public class LoggedInUserResponse : BaseResponse
{
   public string? AccessToken { get; set; }
   public string? RefreshToken { get; set; }
}