using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using PetShop.Core.ErrorHandling;

namespace PetShop.Core.ApplicationService.Implementation
{
    public class PetService : IPetService
    {
        IPetRepository _petRepo;
        IErrorFactory _errorFactory;
        public PetService(IPetRepository petRepo, IErrorFactory errorFactory)
        {
            _petRepo = petRepo;
            _errorFactory = errorFactory;
        }

        public Pet CreatePet(Pet pet)
        {
            validatePet(pet);
            return _petRepo.CreatePet(pet);
        }

        public Pet DeletePet(Pet pet)
        {

            return _petRepo.DeletePet(pet);
        }

        public Pet GetPet(int id)
        {
            return _petRepo.readPet(id);
        }

        public List<Pet> GetPets(Filter filter)
        {
            if (filter == null)
            {
                return _petRepo.ReadPets(null).ToList();
            }
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                _errorFactory.Invalid(message: "Current page and items per page index must be 0 or more");
            }
            if ((filter.CurrentPage - 1 * filter.ItemsPrPage) >= _petRepo.Count())
            {
                _errorFactory.Invalid(message: "Index out of bounds. Current page is too high");
            }
            return _petRepo.ReadPets(filter).ToList();
        }

        public Pet UpdatePet(Pet petToUpdate)
        {
            return _petRepo.UpdatePet(petToUpdate);
        }

        private void validatePet(Pet pet)
        {
            if (String.IsNullOrEmpty(pet.name))
            {
                _errorFactory.Invalid(message: "Pet can't not have a name");
            }
            else if (pet.birthDate > pet.soldDate)
            {
                _errorFactory.Invalid(message: "The pets birth cant be after the pet has been sold");
            }
            else if (nameHasNumber(pet.name))
            {
                _errorFactory.Invalid(message: "There cant be numbers in the pets name");
            }

        }

        private bool nameHasNumber(String name)
        {
            bool isNumber = false;
            foreach (Char cha in name.ToCharArray(0, name.Length))
            {
                if (char.IsDigit(cha))
                {
                    isNumber = true;
                    break;
                }
            }
            return isNumber;
        }

    }
}
