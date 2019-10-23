using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        List<Pet> GetPets(Filter filter);
        Pet CreatePet(Pet pet);
        Pet DeletePet(Pet pet);
        Pet UpdatePet(Pet petToUpdate);
        Pet GetPet(int id);
    }
}
