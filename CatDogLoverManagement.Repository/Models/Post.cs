using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public int PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int UserId { get; set; }
        public int AnimalId { get; set; }
        public int ServiceId { get; set; }

        public virtual Animal Animal { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
