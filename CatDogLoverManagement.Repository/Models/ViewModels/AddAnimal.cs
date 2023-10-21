using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class AddAnimal
    {
        [Required]
        public string AnimalName { get; set; } = null!;
        [Required]
        public string AnimalType { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public int Age { get; set; }
    }

    
}
