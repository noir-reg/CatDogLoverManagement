using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Service
    {
        public Service()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
            TimeFrames = new HashSet<TimeFrame>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime OpenDate { get; set; }
        public string Status { get; set; } = null!;
        public string Note { get; set; } = null!;
        public string Image { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<TimeFrame> TimeFrames { get; set; }
    }
}
