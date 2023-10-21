using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class Register
    {
        public string Username { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public bool Action { get; set; }
        public Guid RoleId { get; set; }
    }
}
