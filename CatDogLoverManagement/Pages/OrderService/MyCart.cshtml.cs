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
    public class MyCartModel : PageModel
    {
        private readonly IOrderServiceRepo orderServiceRepository;
        private readonly StripeSetting _stripeSettings;
        public MyCartModel(IOrderServiceRepo context, IOptions<StripeSetting> stripeSettings)
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
        public async Task OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                RedirectToPage("/Login");
            }
            Order = await orderServiceRepository.GetAllOrderedAsync(userId, null);
        }

        public async Task<IActionResult> OnPostPayOrderAsync()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }
            if (OrderItemId == null)
            {
                return Page();
            }

            var currency = "VND";
            var successUrl = "https://localhost:7045/OrderService/StripeSuccess"; 
            var cancelUrl = "https://localhost:7045/OrderService/StripeCancel";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions> {
            new SessionLineItemOptions {
                PriceData = new SessionLineItemPriceDataOptions {
                    Currency = currency,
                    UnitAmount = Convert.ToInt32(Price) * 100, // Amount in the smallest currency unit (e.g., cents)
                    ProductData = new SessionLineItemPriceDataProductDataOptions {
                        Name = "Product Name",
                        Description = "Product Description"
                    }
                },
                Quantity = 1
            }
        },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);
            SessionId = session.Id;
            Order = await orderServiceRepository.GetAllOrderedAsync(userId, null);

            // Store the OrderItemId in session or TempData so that you can access it in the Stripe webhook handler
            TempData["OrderItemId"] = OrderItemId.ToString();
            TempData["success"] = "Payment order successfully";
            return Redirect(session.Url);
        }
    }
}
