using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureAPI.Data;
using NatureAPI.Models;

namespace NatureAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly NatureDbContext _context;

    public ReviewsController(NatureDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        return await _context.Reviews.Include(r => r.Place).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _context.Reviews.Include(r => r.Place).FirstOrDefaultAsync(r => r.Id == id);
        if (review == null) return NotFound();
        return review;
    }

    [HttpPost]
    public async Task<ActionResult<Review>> CreateReview(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
    }
}