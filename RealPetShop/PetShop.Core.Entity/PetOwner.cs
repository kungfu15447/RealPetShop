using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entity
{
    public class PetOwner
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
