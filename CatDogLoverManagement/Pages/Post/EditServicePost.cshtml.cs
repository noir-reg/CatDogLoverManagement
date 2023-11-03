using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class EditServicePostModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IAnimalRepository animalRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly ITimeFrameRepository timeFrameRepository;
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        [BindProperty]
        public Service Service { get; set; }
        [BindProperty]
        public TimeFrame TimeFrame { get; set; }
        public IFormFile FeaturedImage { get; set; }
        public EditServicePostModel(IBlogPostRepository blogPostRepository, IAnimalRepository animalRepository,
            IServiceRepository serviceRepository, ITimeFrameRepository timeFrameRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.animalRepository = animalRepository;
            this.serviceRepository = serviceRepository;
            this.timeFrameRepository = timeFrameRepository;

        }
        public async Task OnGet(Guid id)
        {
            BlogPost = await blogPostRepository.GetAsync(id);
            if (BlogPost.ServiceId != null)
                Service = await serviceRepository.GetAsync((Guid)BlogPost.ServiceId);
            if (Service != null)
            {
                TimeFrame = await timeFrameRepository.GetAsync(Service.ServiceId);
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                await blogPostRepository.UpdateAsync(BlogPost);
                await timeFrameRepository.UpdateAsync(TimeFrame);
                await serviceRepository.UpdateAsync(Service);
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

                return RedirectToPage("MySellOrGivePosts");
            }

            return Page();
        }
    }
}
