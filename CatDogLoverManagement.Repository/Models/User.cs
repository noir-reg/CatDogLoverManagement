using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class User
    {
        public User()
        {
            BlogPosts = new HashSet<BlogPost>();
            Comments = new HashSet<Comment>();
            OrderBuyers = new HashSet<Order>();
            OrderSellers = new HashSet<Order>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public Guid RoleId { get; set; }

        public virtual UserRole Role { get; set; } = null!;
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> OrderBuyers { get; set; }
        public virtual ICollection<Order> OrderSellers { get; set; }
    }
}
