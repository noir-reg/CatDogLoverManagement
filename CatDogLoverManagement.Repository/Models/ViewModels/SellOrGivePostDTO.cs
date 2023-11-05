using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class SellOrGivePostDTO
    {
        public Guid PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = null!;
        public string Image { get; set; } = null!;
        public Guid? UserId { get; set; }
        public Guid? AnimalId { get; set; }
        public string AnimalName { get; set; } = null!;
        public string AnimalType { get; set; } = null!;
        public string AnimalDescription { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public ICollection<CommentDTO>? Comments = new Collection<CommentDTO>();
    }
}
