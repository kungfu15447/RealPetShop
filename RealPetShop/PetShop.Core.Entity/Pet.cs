using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entity
{
    public class Pet
    {
        public int id { get; set; }
        public string name { get; set; }
        public PetTypes type { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime soldDate { get; set; }
        public string color { get; set; }
        public double price { get; set; }
        public List<PetOwner> ownersHistory { get; set; }
    }
}
