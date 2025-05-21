using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Index = System.Index;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? ImagePath { get; set; }
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime2")]
    public DateTime? EventDate { get; set; }
    public string? Description { get; set; } 

    public int EventAddressId { get; set; }
    public EventAddressEntity EventAddress { get; set; } = null!;

    public ICollection<EventPackageEntity> Packages { get; set; } = [];
}
