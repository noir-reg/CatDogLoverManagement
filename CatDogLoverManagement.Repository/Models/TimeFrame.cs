using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class TimeFrame
    {
        public int Id { get; set; }
        public string Time { get; set; } = null!;
        public int? ServiceId { get; set; }

        public virtual Service? Service { get; set; }
    }
}
