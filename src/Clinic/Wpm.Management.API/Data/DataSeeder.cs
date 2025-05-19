using Wpm.Management.API.Models;

namespace Wpm.Management.API.Data;

public class DataSeeder(ManagementDbContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (!context.Breeds.Any())
        {
            var breeds = new List<Breed>
            {
                new() { Id = Guid.NewGuid(), Name = "Labrador" },
                new() { Id = Guid.NewGuid(), Name = "Bulldog" },
                new() { Id = Guid.NewGuid(), Name = "Poodle" }
            };

            await context.Breeds.AddRangeAsync(breeds);

            var random = new Random();
            var petNames = new[]
            {
                "Buddy", "Charlie", "Max", "Bella", "Lucy",
                "Daisy", "Luna", "Rocky", "Molly", "Coco"
            };

            var pets = petNames.Select(name => new Pet
            {
                Id = Guid.NewGuid(),
                Name = name,
                Age = random.Next(1, 15),
                BreedId = breeds[random.Next(breeds.Count)].Id
            }).ToList();

            await context.Pets.AddRangeAsync(pets);
            await context.SaveChangesAsync();
        }
    }
}