namespace NatureAPI.Models;
using System.Collections.Generic;

public class Amenity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; // e.g. "Baños", "Estacionamiento"

    // Navigation
    public ICollection<PlaceAmenity> PlaceAmenities { get; set; } = new List<PlaceAmenity>();
}
