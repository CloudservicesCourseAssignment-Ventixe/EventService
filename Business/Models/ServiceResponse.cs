namespace Business.Models;

public class ServiceResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public string? Error { get; set; }
}

public class ServiceResponse<T> : ServiceResponse
{
    public T? Result { get; set; }
}
