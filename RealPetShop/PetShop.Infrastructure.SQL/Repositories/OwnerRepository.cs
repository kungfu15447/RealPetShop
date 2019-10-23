using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastructure.SQL.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private PetShopContext _context;

        public OwnerRepository(PetShopContext context)
        {
            _context = context;
        }
        public Owner AddOwner(Owner owner)
        {
            _context.Attach(owner).State = EntityState.Added;
            _context.SaveChanges();
            return owner;
        }

        public int Count()
        {
            return _context.Owners.Count();
        }

        public Owner DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            _context.SaveChanges();
            return owner;
        }

        public Owner ReadOwner(int id)
        {
            return _context.Owners
                .Include(o => o.petHistory)
                .ThenInclude(po => po.Pet)
                .FirstOrDefault(o => o.id == id);
        }

        public IEnumerable<Owner> ReadOwners(Filter filter)
        {
            if (filter != null && filter.CurrentPage > 0 && filter.ItemsPrPage > 0)
            {
                var filteredList = _context.Owners
                    .Include(o => o.petHistory)
                    .ThenInclude(po => po.Pet)
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
                return filteredList.ToList();
            }
            return _context.Owners.
                Include(o => o.petHistory)
                .ThenInclude(po => po.Pet)
                .ToList();
        }

        public Owner UpdateOwner(Owner toBeUpdated, Owner updatedOwner)
        {
            _context.Attach(toBeUpdated).State = EntityState.Modified;
            var petOwners = new List<PetOwner>(toBeUpdated.petHistory ?? new List<PetOwner>());
            _context.PetOwners.RemoveRange(
                _context.PetOwners.Where(po => po.OwnerId == toBeUpdated.id)
             );
            foreach (var po in petOwners)
            {
                _context.Entry(po).State = EntityState.Added;
            }
            _context.SaveChanges();
            return toBeUpdated;
        }
    }
}
