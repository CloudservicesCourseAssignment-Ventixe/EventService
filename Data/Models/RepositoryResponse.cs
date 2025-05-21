namespace Data.Models;

public class RepositoryResponse
{
    public bool Succeeded { get; set; } 
    public string? Message { get; set; }
    public string? Error { get; set; }
}

public class RepositoryResponse<T> : RepositoryResponse
{
    public T? Result { get; set; }
}

