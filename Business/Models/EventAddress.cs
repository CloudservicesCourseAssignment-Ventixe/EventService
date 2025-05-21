namespace Business.Models;

public class EventAddress
{
    public int Id { get; set; }
    public string Arena { get; set; } = null!;
    public string? City { get; set; }
    public string? State { get; set; }
}
