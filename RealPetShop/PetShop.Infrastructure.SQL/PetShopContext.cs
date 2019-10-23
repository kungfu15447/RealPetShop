using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.SQL
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasMany(p => p.ownersHistory)
                .WithOne(po => po.Pet)
                .HasForeignKey(po => po.PetId);
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.petHistory)
                .WithOne(po => po.Owner)
                .HasForeignKey(po => po.OwnerId);
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }

    }
}
