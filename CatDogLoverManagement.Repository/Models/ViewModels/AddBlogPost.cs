
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class AddBlogPost
    {
        public string Title { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = null!;
        public string Image { get; set; } = null!;
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid AnimalId { get; set; } = Guid.Empty;
        public Guid ServiceId { get; set; } = Guid.Empty;
    }
}

