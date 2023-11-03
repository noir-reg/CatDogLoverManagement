using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class ServicePostDTO
    {
        public Guid PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; } = null!;
        public Guid? UserId { get; set; }        
        public Guid? ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ServiceNote { get; set; } = null!;     
        public DateTime OpenDate { get; set; }
        public string Status { get; set; } = null!;
        public Guid Id { get; set; }
        /*public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }*/

        public ICollection<TimeFrame> TimeFrames = new Collection<TimeFrame>();

        public ICollection<CommentDTO>? Comments = new Collection<CommentDTO>();
    }
}
