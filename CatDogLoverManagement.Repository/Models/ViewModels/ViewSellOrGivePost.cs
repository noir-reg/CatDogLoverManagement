using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class ViewSellOrGivePost
    {
        public Guid AnimalId { get; set; }
        public string AnimalName { get; set; } = null!;
        public string AnimalType { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
    }
}
