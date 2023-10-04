using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Animal
    {
        public Animal()
        {
            Posts = new HashSet<Post>();
        }

        public int AnimalId { get; set; }
        public string AnimalName { get; set; } = null!;
        public string AnimalType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
