using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IOwnerService
    {
        Owner CreateOwner(Owner owner);
        Owner DeleteOwner(Owner owner);
        Owner GetOwner(int id);
        List<Owner> GetAllOwners(Filter filter);
        Owner UpdateOwner(Owner toBeUpdated, Owner updatedOwner);
    }
}
