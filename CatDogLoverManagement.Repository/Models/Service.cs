using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Service
    {
        public Service()
        {
            BlogPosts = new HashSet<BlogPost>();
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
            TimeFrames = new HashSet<TimeFrame>();
        }

        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime OpenDate { get; set; }
        public string Status { get; set; } = null!;
        public string Note { get; set; } = null!;
        public string Image { get; set; } = null!;

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<TimeFrame> TimeFrames { get; set; }
    }
}
