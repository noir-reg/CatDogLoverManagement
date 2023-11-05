using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class ListModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IOrderRepository orderRepository;
        public IEnumerable<SellOrGivePostDTO> BlogPosts { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string VolunteerId { get; set; }
        [BindProperty]
        public string PostId { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        public ListModel(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository, IOrderRepository orderRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.commentRepository = commentRepository;
            this.orderRepository = orderRepository;
        }
        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
            BlogPosts = await blogPostRepository.GetAllSellPostAsync();
        }
        public async Task<IActionResult> OnPost()
        {
            var id = HttpContext.Session.GetString("userId");

            if (!string.IsNullOrEmpty(id))
            {
                var AnimalId = await blogPostRepository.GetAnimalId(PostId);

                var result = await orderRepository.CreateOrderForSellOrGivePost((Guid)Order.AnimalId, Order.SellerId.ToString(), id, (decimal)Order.Price);

                TempData["success"] = "Order successfully";
                return RedirectToPage("SellOrGivePosts");
            }

            return RedirectToPage("/Login");

        }
        public async Task<IActionResult> OnPostAddComment()
        {
            if (string.IsNullOrEmpty(VolunteerId))
                return RedirectToPage("/Login");
            
            var result = await commentRepository.AddAsync(Message, PostId, VolunteerId);
            if (result)
            {
                TempData["success"] = "Completed";
            }
            return RedirectToPage("SellOrGivePosts");
        }
    }
}
