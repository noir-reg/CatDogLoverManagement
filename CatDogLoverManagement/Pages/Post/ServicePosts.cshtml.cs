using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class ServicePostModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICommentRepository commentRepository;
        public IEnumerable<ServicePostDTO> BlogPosts { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string VolunteerId { get; set; }
        [BindProperty]
        public string PostId { get; set; }
        public ServicePostModel(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.commentRepository = commentRepository;
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
        public async Task<IActionResult> OnPostAddComment()
        {
            if (string.IsNullOrEmpty(VolunteerId))
                return BadRequest("You haven't logined");
            var result = await commentRepository.AddAsync(Message, PostId, VolunteerId);          
            return RedirectToPage("ServicePosts");
        }
    }
}
