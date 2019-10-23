using PetShop.Core.Entity;
using PetShop.Infrastructure.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.RestAPI.Initializer
{
    public class DBInitializer
    {
        public static void Seed(PetShopContext ctx)
        {
            Pet pet1 = new Pet
            {
                name = "Rex",
                type = PetTypes.Dog,
                birthDate = new DateTime(2017, 4, 20),
                soldDate = new DateTime(2017, 6, 25),
                color = "Brownish",
                price = 275,
                ownersHistory = new List<PetOwner>()
            };
            Pet pet2 = new Pet
            {
                name = "Spoofy",
                type = PetTypes.Cat,
                birthDate = new DateTime(2018, 3, 10),
                soldDate = new DateTime(2018, 3, 30),
                color = "White and black",
                price = 550,
                ownersHistory = new List<PetOwner>()
            };
            Owner owner1 = new Owner
            {
                firstName = "Simon",
                lastName = "Kjær",
                address = "Stengårdsvej 12",
                petHistory = new List<PetOwner>()
            };
            Owner owner2 = new Owner
            {
                firstName = "Levis",
                lastName = "Kjongaard",
                address = "Hjertingvej 5",
                petHistory = new List<PetOwner>()
            };



            owner1 = ctx.Owners.Add(owner1).Entity;
            owner2 = ctx.Owners.Add(owner2).Entity;

            PetOwner PetOwner1 = new PetOwner
            {
                Owner = owner1
            };

            PetOwner PetOwner2 = new PetOwner
            {
                Owner = owner2
            };

            PetOwner PetOwner3 = new PetOwner
            {
                Owner = owner1
            };

            pet1.ownersHistory.Add(PetOwner1);
            pet1.ownersHistory.Add(PetOwner2);
            pet2.ownersHistory.Add(PetOwner3);

            ctx.Pets.Add(pet1);
            ctx.Pets.Add(pet2);



            ctx.SaveChanges();
        }
    }
}
