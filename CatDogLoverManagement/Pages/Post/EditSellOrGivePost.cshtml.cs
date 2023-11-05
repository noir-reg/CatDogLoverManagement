using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;

using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class EditModel : PageModel
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
        public EditModel(IBlogPostRepository blogPostRepository, IAnimalRepository animalRepository,
            IServiceRepository serviceRepository, ITimeFrameRepository timeFrameRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.animalRepository = animalRepository;
            this.serviceRepository = serviceRepository;
            this.timeFrameRepository = timeFrameRepository;

        }
        public async Task OnGet(string id)
        {
            BlogPost = await blogPostRepository.GetAsync(Guid.Parse(id));
            if (BlogPost.AnimalId != null)
                Animal = await animalRepository.GetAsync((Guid)BlogPost.AnimalId);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                await blogPostRepository.UpdateAsync(BlogPost);
                await animalRepository.UpdateAsync(Animal);
                TempData["success"] = "Update successfully";
            }
            catch (Exception ex)
            {
               
            }


            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostRepository.DeleteAsync(BlogPost.PostId);
            if (deleted)
            {
                TempData["success"] = "Delete successfully";

                return RedirectToPage("MySellOrGivePosts");
            }

            return Page();
        }
    }
}
