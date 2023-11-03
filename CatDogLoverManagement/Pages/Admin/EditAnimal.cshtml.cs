using CatDogLoverManagement.Pages.Post;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Admin
{
    public class EditAnimalModel : PageModel
    {

        private readonly IBlogPostRepository blogPostRepository;
        private readonly IAnimalRepository animalRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly ITimeFrameRepository timeFrameRepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }
        [BindProperty]
        public Animal Animal { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        public EditAnimalModel(IBlogPostRepository blogPostRepository, IAnimalRepository animalRepository,
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
            if (BlogPost.AnimalId != null)
                Animal = await animalRepository.GetAsync((Guid)BlogPost.AnimalId);
        }


        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                await blogPostRepository.UpdateAdminAsync(BlogPost);
                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated successfully!",
                    type = Repository.Models.Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                await blogPostRepository.UpdateAdminAsync(BlogPost);
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
