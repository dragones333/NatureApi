namespace NatureAPI.Models;
using System;
public class Review
{
    public int Id { get; set; }
    public int PlaceId { get; set; }
    public string Author { get; set; } = null!;
    public int Rating { get; set; } // 1..5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Place? Place { get; set; }
}
