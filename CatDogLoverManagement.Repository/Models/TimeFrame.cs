using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class TimeFrame
    {
        public Guid Id { get; set; }
        public string Time { get; set; } = null!;
        public Guid ServiceId { get; set; }

        public virtual Service Service { get; set; } = null!;
    }
}
