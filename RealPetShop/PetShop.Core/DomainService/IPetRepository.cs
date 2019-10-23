using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets(Filter filter);
        Pet CreatePet(Pet pet);
        Pet DeletePet(Pet pet);
        Pet UpdatePet(Pet petToUpdate);
        Pet readPet(int id);
        int Count();
    }
}
