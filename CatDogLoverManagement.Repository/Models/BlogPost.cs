using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class BlogPost
    {
        public BlogPost()
        {
            Comments = new HashSet<Comment>();
        }

        public Guid PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = null!;
        public string Image { get; set; } = null!;
        public Guid? UserId { get; set; }
        public Guid? AnimalId { get; set; }
        public Guid? ServiceId { get; set; }

        public virtual Animal? Animal { get; set; }
        public virtual Service? Service { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
