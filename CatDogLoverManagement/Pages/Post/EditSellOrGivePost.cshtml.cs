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
        public EditBlogPostRequest BlogPost { get; set; }

        [BindProperty]
        public EditAnimal Animal { get; set; }
        [BindProperty]
        public IFormFile? FeaturedImage { get; set; }
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
            var blogPostDomainModel = await blogPostRepository.GetAsync(Guid.Parse(id));
            if (blogPostDomainModel != null)
            {
                BlogPost = new EditBlogPostRequest
                {
                    PostId = blogPostDomainModel.PostId,
                    Title = blogPostDomainModel.Title,
                    Description = blogPostDomainModel.Description,
                    Price = blogPostDomainModel.Price,
                    CreatedDate = blogPostDomainModel.CreatedDate,
                    Status = blogPostDomainModel.Status,
                    Image = blogPostDomainModel.Image,
                    UserId = blogPostDomainModel.UserId,
                    AnimalId = blogPostDomainModel.AnimalId,
                    ServiceId = blogPostDomainModel.ServiceId,
                };
                if (blogPostDomainModel.AnimalId != null)
                {
                    var blogAnimalPostDomainModel = await animalRepository.GetAsync((Guid)BlogPost.AnimalId);
                    if (blogAnimalPostDomainModel != null)
                    {
                        Animal = new EditAnimal
                        {
                            AnimalId = blogAnimalPostDomainModel.AnimalId,
                            AnimalName = blogAnimalPostDomainModel.AnimalName,
                            AnimalType = blogAnimalPostDomainModel.AnimalType,
                            Description = blogAnimalPostDomainModel.Description,
                            Gender = blogAnimalPostDomainModel.Gender,
                            Age = blogAnimalPostDomainModel.Age,

                        };
                    }
                }
            }
        }
            
        
        public async Task<IActionResult> OnPostEdit()
        {
        if(ModelState.IsValid)
            {
                try
                {

                    var blogPostDomainModel = new BlogPost
                    {
                        PostId = BlogPost.PostId,
                        Title = BlogPost.Title,
                        Description = BlogPost.Description,
                        Price = BlogPost.Price,
                        CreatedDate = DateTime.Now,
                        Status = BlogPost.Status,
                        Image = BlogPost.Image,
                        UserId = BlogPost.UserId,
                        ServiceId = BlogPost.ServiceId,
                        AnimalId = BlogPost.AnimalId,



                    };

                    var blogAnimal = new Animal
                    {
                        AnimalId = Animal.AnimalId,
                        AnimalName = Animal.AnimalName,
                        AnimalType = Animal.AnimalType,
                        Description = Animal.Description,
                        Gender = Animal.Gender,
                        Age = Animal.Age,
                    };

                    await blogPostRepository.UpdateAsync(blogPostDomainModel);
                    await animalRepository.UpdateAsync(blogAnimal);
                    TempData["success"] = "Update successfully";
                }
                catch (Exception ex)
                {

                }
                return Page();
            }
            else
            {
                return Page();
            }


            
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
