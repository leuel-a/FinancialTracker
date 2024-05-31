namespace ft.transaction_management.Application.Models;

public class DatabaseOperationResult
{
    public int? Id { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
    public string[]? ErrorMessages { get; set; }


    public static DatabaseOperationResult Success()
    {
        return new DatabaseOperationResult { Succeeded = true };
    }

    public static DatabaseOperationResult Failure(string[] errors, string message)
    {
        return new DatabaseOperationResult { Succeeded = false, ErrorMessages = errors, Message = message };
    }
}