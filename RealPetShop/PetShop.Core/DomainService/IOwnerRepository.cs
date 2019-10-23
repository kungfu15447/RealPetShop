using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {
        Owner AddOwner(Owner owner);
        Owner DeleteOwner(Owner owner);
        IEnumerable<Owner> ReadOwners(Filter filter);
        Owner ReadOwner(int id);
        Owner UpdateOwner(Owner toBeUpdated, Owner updatedOwner);
        int Count();
    }
}
