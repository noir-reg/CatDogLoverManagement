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
using CatDogLoverManagement.Repository.Models.Enums;

namespace CatDogLoverManagement.Pages.OrderService
{
    public class StripeSuccess : PageModel
    {
        private readonly IOrderServiceRepo orderServiceRepository;
        private readonly StripeSetting _stripeSettings;
        public StripeSuccess(IOrderServiceRepo context, IOptions<StripeSetting> stripeSettings)
        {
            orderServiceRepository = context;
            _stripeSettings = stripeSettings.Value;
        }
        public async Task<IActionResult> OnGet()
        {
            var orderItemId = TempData["OrderItemId"]?.ToString();
            TempData["success"] = "Payment order successfully";
            if (string.IsNullOrEmpty(orderItemId))
            {
                // Handle missing or invalid orderItemId
                return RedirectToPage("/Error");
            }

            // Here, you can update the database, set the payment status, or perform any other necessary actions
            var order = await orderServiceRepository.GetByIdAsync(Guid.Parse(orderItemId));
            order.Status = OrderStatusConst.SUCCESS;
            var result = await orderServiceRepository.UpdateAsync(order);
            if (result)
            {
                TempData["success"] = "Payment order successfully";
            } else
            {
                TempData["error"] = "Payment order failure";
                return RedirectToPage("/StripeCancel");
            }
            return Page(); // Return to the original page after handling the payment success
        }
    }
}
