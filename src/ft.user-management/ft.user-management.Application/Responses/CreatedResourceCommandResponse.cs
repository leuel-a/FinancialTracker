namespace ft.user_management.Application.Responses;

public class CreatedResourceCommandResponse<T> : BaseCommandResponse
{
    public T? Resource { get; set; }
}