using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class ServiceModel
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime OpenDate { get; set; }
        public string Status { get; set; } = null!;
        public string Note { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
