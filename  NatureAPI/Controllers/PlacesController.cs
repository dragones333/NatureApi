using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureAPI.Data;
using NatureAPI.Models;

namespace NatureAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly NatureDbContext _context;

    public PlacesController(NatureDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> GetPlaces([FromQuery] string? category, [FromQuery] string? difficulty)
    {
        var query = _context.Places
            .Include(p => p.Trails)
            .Include(p => p.Photos)
            .Include(p => p.Reviews)
            .Include(p => p.PlaceAmenities)
                .ThenInclude(pa => pa.Amenity)
            .AsQueryable();

        if (!string.IsNullOrEmpty(category))
            query = query.Where(p => p.Category == category);

        if (!string.IsNullOrEmpty(difficulty))
            query = query.Where(p => p.Trails.Any(t => t.Difficulty == difficulty));

        return await query.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Place>> GetPlace(int id)
    {
        var place = await _context.Places
            .Include(p => p.Trails)
            .Include(p => p.Photos)
            .Include(p => p.Reviews)
            .Include(p => p.PlaceAmenities)
                .ThenInclude(pa => pa.Amenity)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (place == null) return NotFound();

        return place;
    }
    
    [HttpPost]
    public async Task<ActionResult<Place>> CreatePlace(Place place)
    {
        if (place.Latitude < -90 || place.Latitude > 90 ||
            place.Longitude < -180 || place.Longitude > 180)
            return BadRequest("Coordenadas inválidas.");

        place.CreatedAt = DateTime.UtcNow;
        _context.Places.Add(place);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlace), new { id = place.Id }, place);
    }
}
