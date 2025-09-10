using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureAPI.Data;
using NatureAPI.Models;

namespace NatureAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly NatureDbContext _context;

    public PhotosController(NatureDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
    {
        return await _context.Photos.Include(p => p.Place).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Photo>> GetPhoto(int id)
    {
        var photo = await _context.Photos.Include(p => p.Place).FirstOrDefaultAsync(p => p.Id == id);
        if (photo == null) return NotFound();
        return photo;
    }

    [HttpPost]
    public async Task<ActionResult<Photo>> CreatePhoto(Photo photo)
    {
        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
    }
}