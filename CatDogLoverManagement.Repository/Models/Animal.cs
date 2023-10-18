using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Animal
    {
        public Animal()
        {
            BlogPosts = new HashSet<BlogPost>();
            Orders = new HashSet<Order>();
        }

        public Guid AnimalId { get; set; }
        public string AnimalName { get; set; } = null!;
        public string AnimalType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
