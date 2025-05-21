using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Arena), IsUnique = true)]
public class EventAddressEntity
{
    public int Id { get; set; }
    public string Arena { get; set; } = null!;
    public string? City { get; set; } 
    public string? State { get; set; } 
    public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
}
