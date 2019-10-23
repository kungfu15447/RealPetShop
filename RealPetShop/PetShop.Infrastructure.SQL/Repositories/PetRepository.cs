using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastructure.SQL.Repositories
{
    public class PetRepository : IPetRepository
    {
        private PetShopContext _context;

        public PetRepository(PetShopContext context)
        {
            _context = context;
        }

        public int Count()
        {
            return _context.Pets.Count();
        }

        public Pet CreatePet(Pet pet)
        {
            _context.Attach(pet).State = EntityState.Added;
            _context.SaveChanges();
            return pet;
        }

        public Pet DeletePet(Pet pet)
        {
            //Pet petRemoved = _context.Remove(new Pet { id = pet.id}).Entity;
            _context.Remove(pet);
            _context.SaveChanges();
            return pet;
        }

        public Pet readPet(int id)
        {
            return _context.Pets
                .Include(o => o.ownersHistory)
                .ThenInclude(po => po.Owner)
                .FirstOrDefault(p => p.id == id);
        }

        public IEnumerable<Pet> ReadPets(Filter filter)
        {
            if (filter != null && filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                var filteredList = _context.Pets
                    .Include(o => o.ownersHistory)
                    .ThenInclude(po => po.Owner)
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
                return filteredList.ToList();
            }
            return _context.Pets
                .Include(o => o.ownersHistory)
                .ThenInclude(po => po.Owner).ToList();
        }

        public Pet UpdatePet(Pet petToUpdate)
        {
            _context.Attach(petToUpdate).State = EntityState.Modified;
            // _context.Entry(petToUpdate).Collection(p => p.ownersHistory).IsModified = true;

            var petOwners = new List<PetOwner>(petToUpdate.ownersHistory ?? new List<PetOwner>());
            _context.PetOwners.RemoveRange(
                _context.PetOwners.Where(p => p.PetId == petToUpdate.id)
             );
            foreach (var po in petOwners)
            {
                _context.Entry(po).State = EntityState.Added;
            }
            _context.SaveChanges();
            return petToUpdate;
        }
    }
}
