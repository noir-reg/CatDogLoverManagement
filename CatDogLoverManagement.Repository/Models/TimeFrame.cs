using System;
using System.Collections.Generic;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class TimeFrame
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
        public virtual Service? Service { get; set; }
    }
}
