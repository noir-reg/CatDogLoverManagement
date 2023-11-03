using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Repositories;

namespace CatDogLoverManagement.Pages.OrderService
{
    public class CreateModel : PageModel
    {
        private readonly IOrderServiceRepo _context;

        public CreateModel(IOrderServiceRepo context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
       
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return BadRequest("You haven't logined");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
