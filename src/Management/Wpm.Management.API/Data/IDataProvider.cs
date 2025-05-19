using Wpm.Management.API.Models;

namespace Wpm.Management.API.Data;

public interface IDataProvider
{
    List<Breed> GetBreeds();
    
    List<Pet> GetPets();
    
    Pet? GetPetById(Guid id);
    
    void AddPet(Pet pet);
    
    Breed? GetBreedById(Guid id);
    
    void AddBreed(Breed breed);
}