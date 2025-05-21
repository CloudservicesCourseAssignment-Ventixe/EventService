using Data.Entities;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class CreateEventRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImagePath { get; set; }

    [Column("DateTime2")]
    public DateTime? EventDate { get; set; }

    public string Arena { get; set; } = null!;
    public string? City { get; set; }
    public string? State { get; set; }
}
