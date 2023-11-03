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
using CatDogLoverManagement.Repository.Models.Enums;

namespace CatDogLoverManagement.Pages.OrderService
{
    public class ManageOrdersModel : PageModel
    {
        private readonly IOrderServiceRepo orderServiceRepository;

        public ManageOrdersModel(IOrderServiceRepo context)
        {
            orderServiceRepository = context;
        }

        public IEnumerable<ViewOrderService> Order { get;set; } = default!;

        [BindProperty]
        public Guid OrderItemId {  get; set; }
        public async Task OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                RedirectToPage("/Login");
            }
            Order = await orderServiceRepository.GetAllOrderedAsync(null, userId);
        }

        public async Task<IActionResult> OnPostApproveOrderAsync()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                RedirectToPage("/Login");
            }
            if(OrderItemId == null)
            {
                return Page();
            }
            Order order = await orderServiceRepository.GetByIdAsync(OrderItemId);
            if (order == null)
            {
                return Page();
            }
            order.Status = OrderStatusConst.APPROVED;
            bool result  = await orderServiceRepository.UpdateAsync(order);
            if (result)
            {
                Order = await orderServiceRepository.GetAllOrderedAsync(null, userId);
            }
            Order = await orderServiceRepository.GetAllOrderedAsync(null, userId);
            return Page();
        }
    }
}
