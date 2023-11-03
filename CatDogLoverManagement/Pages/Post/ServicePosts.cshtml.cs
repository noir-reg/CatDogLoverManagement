using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class ServicePostModel : PageModel, IValidatableObject
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IOrderServiceRepo orderServiceRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ITimeFrameRepository timeFrameRepository;
        public IEnumerable<ServicePostDTO> BlogPosts { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string VolunteerId { get; set; }
        [BindProperty]
        public string PostId { get; set; }

        [BindProperty]
        public string SelectedTimeFrame { get; set; }

        [BindProperty]
        public Order OrderService { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public ServicePostModel(IBlogPostRepository blogPostRepository, IOrderServiceRepo OrderServiceRepository, ICommentRepository commentRepository, ITimeFrameRepository timeFrameRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.orderServiceRepository = OrderServiceRepository;
            this.commentRepository = commentRepository;
            this.timeFrameRepository = timeFrameRepository;
        }
        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
            BlogPosts = await blogPostRepository.GetAllServicePostAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }
            if (SelectedTimeFrame == null)
            {
                ModelState.AddModelError("SelectedTimeFrame", "Please select a time frame.");
                return RedirectToPage("ServicePosts");
            }
            var timeFrame = await timeFrameRepository.GetTimeFrameByIdAsync(Guid.Parse(SelectedTimeFrame));
            await orderServiceRepository.AddOrderServiceAsync(userId, OrderService.SellerId.ToString(), OrderService.ServiceId.ToString(), timeFrame);
            return RedirectToPage("ServicePosts");
        }

        public async Task<IActionResult> OnPostAddComment()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }
            var result = await commentRepository.AddAsync(Message, PostId, VolunteerId);
            return RedirectToPage("ServicePosts");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SelectedTimeFrame == null)
            {
                yield return new ValidationResult("Please select a time frame.");
            }
        }
    }
}
