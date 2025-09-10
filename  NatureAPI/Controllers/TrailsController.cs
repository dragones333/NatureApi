using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureAPI.Data;
using NatureAPI.Models;

namespace NatureAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrailsController : ControllerBase
{
    private readonly NatureDbContext _context;

    public TrailsController(NatureDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trail>>> GetTrails()
    {
        return await _context.Trails.Include(t => t.Place).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Trail>> GetTrail(int id)
    {
        var trail = await _context.Trails.Include(t => t.Place).FirstOrDefaultAsync(t => t.Id == id);
        if (trail == null) return NotFound();
        return trail;
    }

    [HttpPost]
    public async Task<ActionResult<Trail>> CreateTrail(Trail trail)
    {
        _context.Trails.Add(trail);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTrail), new { id = trail.Id }, trail);
    }
}