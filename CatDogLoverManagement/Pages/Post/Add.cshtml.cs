
using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class AddModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        public AddModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var blogPost = new BlogPost()
            {
                Title = AddBlogPostRequest.Title,
                Description = AddBlogPostRequest.Description,
                Price = AddBlogPostRequest.Price,
                CreatedDate = AddBlogPostRequest.CreatedDate,
                Status = AddBlogPostRequest.Status,
                Image = AddBlogPostRequest.Image,
                UserId = AddBlogPostRequest.UserId,
                AnimalId = AddBlogPostRequest.AnimalId,
                ServiceId = AddBlogPostRequest.ServiceId,
            };
            await blogPostRepository.AddAsync(blogPost);

            var notification = new Notification
            {
                type = Repository.Models.Enums.NotificationType.Success,
                Message = "New blog created!"
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/post/list");


        }
    }
}

