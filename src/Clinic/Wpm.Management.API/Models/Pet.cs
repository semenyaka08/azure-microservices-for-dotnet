namespace Wpm.Management.API.Models;

public class Pet
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public Guid BreedId { get; set; }
    
    public Breed Breed { get; set; } = null!;
}