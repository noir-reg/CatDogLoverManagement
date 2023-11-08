using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class EditService
    {
        [Required]
        public Guid ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public string Note { get; set; } = null!;
        public string? Image { get; set; }

    }
}
