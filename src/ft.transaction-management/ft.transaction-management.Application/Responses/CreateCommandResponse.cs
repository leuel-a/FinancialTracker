namespace ft.transaction_management.Application.Responses;

public class CreateCommandResponse<T> : BaseCommandResponse where T: class
{
    public int Id { get; set; }
    public T? CreatedResource { get; set; }
}