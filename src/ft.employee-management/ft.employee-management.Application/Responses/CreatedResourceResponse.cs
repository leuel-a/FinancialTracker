namespace ft.employee_management.Application.Responses;

public class CreatedResourceResponse<T> : BaseResponse where T : class
{
    public int Id { get; set; }
    public T? Resource { get; set; }
}