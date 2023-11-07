using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Repositories;
using CatDogLoverManagement.Repository.Models.ViewModels;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace CatDogLoverManagement.Pages.OrderService
{
    public class StripeCancel : PageModel
    {
        private readonly IOrderServiceRepo orderServiceRepository;
        private readonly StripeSetting _stripeSettings;
        public StripeCancel(IOrderServiceRepo context, IOptions<StripeSetting> stripeSettings)
        {
            orderServiceRepository = context;
            _stripeSettings = stripeSettings.Value;
        }
        public string SessionId { get ; set; }

        public IEnumerable<ViewOrderService> Order { get; set; } = default!;

        [BindProperty]
        public Guid OrderItemId { get; set; }

        [BindProperty]
        public decimal Price { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var orderItemId = TempData["OrderItemId"]?.ToString();
            TempData["error"] = "Payment order failure";

            if (string.IsNullOrEmpty(orderItemId))
            {
                // Handle missing or invalid orderItemId
                return RedirectToPage("/Error");
            }

            // Here, you can update the database, set the payment status, or perform any other necessary actions

            return Page(); // Return to the original page after handling the payment success
        }
    }
}
