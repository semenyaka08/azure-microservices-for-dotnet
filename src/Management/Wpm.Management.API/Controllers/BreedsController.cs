using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.Management.API.Models;

namespace Wpm.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreedsController(ManagementDbContext context) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBreedById(Guid id)
    {
        var breed = await context.Breeds
            .FirstOrDefaultAsync(b => b.Id == id);
        
        return breed == null ? NotFound() : Ok(breed);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBreeds()
    {
        var breeds = await context.Breeds
            .ToListAsync();
        
        return Ok(breeds);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBreed([FromBody] AddBreedRequest request)
    {
        var breed = new Breed
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        
        await context.Breeds.AddAsync(breed);
        
        await context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetBreedById), new { id = breed.Id }, breed);
    }
}

public record AddBreedRequest(string Name);