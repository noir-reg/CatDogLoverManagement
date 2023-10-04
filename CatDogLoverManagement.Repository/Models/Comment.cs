using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string CommentMessage { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool Ischeck { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int ServiceId { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
