using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class MyServicePostModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        public IEnumerable<ServicePostDTO> BlogPosts { get; set; }

        [BindProperty]
        public Guid BlogPostsItemId { get; set; }

        public MyServicePostModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            var id = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Login");
            }
            BlogPosts = await blogPostRepository.GetAllServicePostAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostRepository.DeleteAsync(BlogPostsItemId);
            if (deleted)
            {
                var notification = new Notification
                {
                    type = Repository.Models.Enums.NotificationType.Success,
                    Message = "Blog was deleted Successfully"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("MyServicePosts");
            }

            return Page();
        }
    }
}
