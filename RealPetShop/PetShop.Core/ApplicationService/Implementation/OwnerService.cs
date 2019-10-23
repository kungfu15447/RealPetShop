using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using PetShop.Core.ErrorHandling;

namespace PetShop.Core.ApplicationService.Implementation
{
    public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepo;
        private IErrorFactory _errorFactory;

        public OwnerService(IOwnerRepository ownerRepo, IErrorFactory errorFactory)
        {
            _ownerRepo = ownerRepo;
            _errorFactory = errorFactory;
        }
        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepo.AddOwner(owner);
        }

        public Owner DeleteOwner(Owner owner)
        {
            return _ownerRepo.DeleteOwner(owner);
        }

        public List<Owner> GetAllOwners(Filter filter)
        {
            if (filter == null)
            {
                return _ownerRepo.ReadOwners(filter).ToList();
            }
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                _errorFactory.Invalid("Current page or items per page can not be below 0");
            }
            if ((filter.CurrentPage - 1) * filter.ItemsPrPage >= _ownerRepo.Count())
            {
                _errorFactory.Invalid("Index out of bounds. Current page is too high");
            }
            return _ownerRepo.ReadOwners(filter).ToList();
        }

        public Owner GetOwner(int id)
        {
            return _ownerRepo.ReadOwner(id);
        }

        public Owner UpdateOwner(Owner toBeUpdated, Owner updatedOwner)
        {
            return _ownerRepo.UpdateOwner(toBeUpdated, updatedOwner);
        }
    }
}
