using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatDogLoverManagement.Repository.Models;

namespace CatDogLoverManagement.Pages.OrderService
{
    public class DetailsModel : PageModel
    {
        private readonly CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext _context;

        public DetailsModel(CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext context)
        {
            _context = context;
        }

      public Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }
    }
}
