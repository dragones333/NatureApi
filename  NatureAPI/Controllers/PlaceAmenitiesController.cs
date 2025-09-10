using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureAPI.Data;
using NatureAPI.Models;

namespace NatureAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceAmenitiesController : ControllerBase
{
    private readonly NatureDbContext _context;

    public PlaceAmenitiesController(NatureDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaceAmenity>>> GetPlaceAmenities()
    {
        return await _context.PlaceAmenities
            .Include(pa => pa.Place)
            .Include(pa => pa.Amenity)
            .ToListAsync();
    }

    [HttpGet("{placeId}/{amenityId}")]
    public async Task<ActionResult<PlaceAmenity>> GetPlaceAmenity(int placeId, int amenityId)
    {
        var pa = await _context.PlaceAmenities
            .Include(p => p.Place)
            .Include(p => p.Amenity)
            .FirstOrDefaultAsync(p => p.PlaceId == placeId && p.AmenityId == amenityId);

        if (pa == null) return NotFound();
        return pa;
    }

    [HttpPost]
    public async Task<ActionResult<PlaceAmenity>> CreatePlaceAmenity(PlaceAmenity pa)
    {
        _context.PlaceAmenities.Add(pa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlaceAmenity), new { placeId = pa.PlaceId, amenityId = pa.AmenityId }, pa);
    }
}