using Microsoft.AspNetCore.Mvc;
using Wpm.Management.API.Data;
using Wpm.Management.API.Models;

namespace Wpm.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(IDataProvider dataProvider) : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetPetById(Guid id)
    {
        var pet = dataProvider.GetPetById(id);
        
        return pet == null ? NotFound() : Ok(pet);
    }

    [HttpGet]
    public IActionResult GetPets()
    {
        var pets = dataProvider.GetPets();

        return Ok(pets);
    }
    
    [HttpPost]
    public IActionResult CreatePet([FromBody] AddPetRequest request)
    {
        var breed = dataProvider.GetBreedById(request.BreedId);
        
        if (breed == null)
            return BadRequest("Breed not found");
        
        var pet = new Pet
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BreedId = request.BreedId
        };
        
        dataProvider.AddPet(pet);
        
        return CreatedAtAction(nameof(GetPetById), new { id = pet.Id }, pet);
    }
}

public record AddPetRequest(
    string Name,
    Guid BreedId
);