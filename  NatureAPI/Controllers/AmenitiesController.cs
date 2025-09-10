using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureAPI.Data;
using NatureAPI.Models;

namespace NatureAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AmenitiesController : ControllerBase
{
    private readonly NatureDbContext _context;

    public AmenitiesController(NatureDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Amenity>>> GetAmenities()
    {
        return await _context.Amenities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Amenity>> GetAmenity(int id)
    {
        var amenity = await _context.Amenities.FindAsync(id);
        if (amenity == null) return NotFound();
        return amenity;
    }

    [HttpPost]
    public async Task<ActionResult<Amenity>> CreateAmenity(Amenity amenity)
    {
        _context.Amenities.Add(amenity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAmenity), new { id = amenity.Id }, amenity);
    }
}