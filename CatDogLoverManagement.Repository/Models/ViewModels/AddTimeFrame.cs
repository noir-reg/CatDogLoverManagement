using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class AddTimeFrame
    {
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
    }
}
