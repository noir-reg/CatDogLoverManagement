using CatDogLoverManagement.Repository.Models.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class Notification
    {
        public string Message { get; set; }
        
        public NotificationType type { get; set; }
    }
}
