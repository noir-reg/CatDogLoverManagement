using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatDogLoverManagement.Repository.Models;

namespace CatDogLoverManagement.Pages.NewFolder
{
    public class animallistModel : PageModel
    {
        private readonly CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext _context;

        public animallistModel(CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext context)
        {
            _context = context;
        }

        public IList<Animal> Animal { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Animals != null)
            {
                Animal = await _context.Animals.ToListAsync();
            }
        }
    }
}
