using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatDogLoverManagement.Repository.Models;

namespace CatDogLoverManagement.Pages.OrderService
{
    public class EditModel : PageModel
    {
        private readonly CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext _context;

        public EditModel(CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order =  await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
           ViewData["AnimalId"] = new SelectList(_context.Animals, "AnimalId", "AnimalName");
           ViewData["BuyerId"] = new SelectList(_context.Users, "UserId", "Email");
           ViewData["SellerId"] = new SelectList(_context.Users, "UserId", "Email");
           ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderExists(Guid id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
