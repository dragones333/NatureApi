namespace NatureAPI.Models;
public class PlaceAmenity
{
    public int PlaceId { get; set; }
    public int AmenityId { get; set; }
    
    public Place? Place { get; set; }
    public Amenity? Amenity { get; set; }
}
