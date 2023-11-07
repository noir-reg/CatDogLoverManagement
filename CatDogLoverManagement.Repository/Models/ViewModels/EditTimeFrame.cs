using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class EditTimeFrame
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ServiceId { get; set; }
        [Required]
        public TimeSpan? From { get; set; }
        [Required]
        public TimeSpan? To { get; set; }
    }
}
