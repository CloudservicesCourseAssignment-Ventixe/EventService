using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public DateTime? EventDate { get; set; }
    public EventAddress EventAddress { get; set; } = null!;
    public ICollection<Package> Packages { get; set; } = [];

}
