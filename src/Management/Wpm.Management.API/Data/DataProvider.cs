using Wpm.Management.API.Models;

namespace Wpm.Management.API.Data;

public class DataProvider : IDataProvider
{
    private readonly List<Breed> _breeds;
    private readonly List<Pet> _pets;

    public DataProvider()
    {
        _breeds =
        [
            new() { Id = Guid.NewGuid(), Name = "Labrador" },
            new() { Id = Guid.NewGuid(), Name = "Bulldog" },
            new() { Id = Guid.NewGuid(), Name = "Poodle" }
        ];

        var random = new Random();
        var petNames = new[]
        {
            "Buddy", "Charlie", "Max", "Bella", "Lucy",
            "Daisy", "Luna", "Rocky", "Molly", "Coco"
        };

        _pets = petNames.Select(name => new Pet
        {
            Id = Guid.NewGuid(),
            Name = name,
            Age = random.Next(1, 15),
            BreedId = _breeds[random.Next(_breeds.Count)].Id
        }).ToList();
    }

    public List<Breed> GetBreeds() => _breeds;
    
    public List<Pet> GetPets() => _pets;
    
    public Pet? GetPetById(Guid id)
    {
        return _pets.FirstOrDefault(p => p.Id == id);
    }

    public void AddPet(Pet pet)
    {
        _pets.Add(pet);
    }

    public Breed? GetBreedById(Guid id)
    {
        return _breeds.FirstOrDefault(b => b.Id == id);
    }

    public void AddBreed(Breed breed)
    {
        _breeds.Add(breed);
    }
}