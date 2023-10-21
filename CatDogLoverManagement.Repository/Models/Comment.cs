using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentMessage { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool Ischeck { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }

        public virtual BlogPost Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
