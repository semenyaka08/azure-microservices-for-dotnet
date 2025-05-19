using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.Management.API.Models;

namespace Wpm.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(ManagementDbContext context) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetById(Guid id)
    {
        var pet = await context.Pets
            .Include(z=>z.Breed)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return pet == null ? NotFound() : Ok(pet);
    }

    [HttpGet]
    public async Task<IActionResult> GetPets()
    {
        var pets = await context.Pets
            .Include(z=>z.Breed)
            .ToListAsync();
        
        return Ok(pets);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePet([FromBody] AddPetRequest request)
    {
        var breed = await context.Breeds
            .FirstOrDefaultAsync(b => b.Id == request.BreedId);
        
        if (breed == null)
            return BadRequest("Breed not found");
        
        var pet = new Pet
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BreedId = request.BreedId
        };
        
        await context.Pets.AddAsync(pet);
        
        await context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetPetById), new { id = pet.Id }, pet);
    }
}

public record AddPetRequest(
    string Name,
    Guid BreedId
);