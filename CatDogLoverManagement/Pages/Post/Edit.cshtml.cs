using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;

using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        public EditModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task OnGet(Guid id)
        {
            BlogPost = await blogPostRepository.GetAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                await blogPostRepository.UpdateAsync(BlogPost);
                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated successfully!",
                    type = Repository.Models.Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                await blogPostRepository.UpdateAsync(BlogPost);
                ViewData["Notification"] = new Notification
                {
                    Message = "Something went wrong!",
                    type = Repository.Models.Enums.NotificationType.Error
                };
            }


            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostRepository.DeleteAsync(BlogPost.PostId);
            if (deleted)
            {
                var notification = new Notification
                {
                    type = Repository.Models.Enums.NotificationType.Success,
                    Message = "Blog was deleted Successfully"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/post/list");
            }

            return Page();
        }
    }
}
