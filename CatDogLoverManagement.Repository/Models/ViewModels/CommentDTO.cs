using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }
        public string CommentMessage { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool Ischeck { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Username { get; set; }
    }
}
