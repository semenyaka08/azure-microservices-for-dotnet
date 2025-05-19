using Microsoft.AspNetCore.Mvc;
using Wpm.Management.API.Data;
using Wpm.Management.API.Models;

namespace Wpm.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreedsController(IDataProvider dataProvider) : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetBreedById(Guid id)
    {
        var breed = dataProvider.GetBreedById(id);
        
        return breed == null ? NotFound() : Ok(breed);
    }
    
    [HttpGet]
    public IActionResult GetBreeds()
    {
        var breeds = dataProvider.GetBreeds();
        
        return Ok(breeds);
    }
    
    [HttpPost]
    public IActionResult CreateBreed([FromBody] AddBreedRequest request)
    {
        var breed = new Breed
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        
        dataProvider.AddBreed(breed);
        
        return CreatedAtAction(nameof(GetBreedById), new { id = breed.Id }, breed);
    }
}

public record AddBreedRequest(string Name);