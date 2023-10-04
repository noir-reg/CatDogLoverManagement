using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            OrderBuyers = new HashSet<Order>();
            OrderSellers = new HashSet<Order>();
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Phonenumber { get; set; }
        public bool Action { get; set; }
        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> OrderBuyers { get; set; }
        public virtual ICollection<Order> OrderSellers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
