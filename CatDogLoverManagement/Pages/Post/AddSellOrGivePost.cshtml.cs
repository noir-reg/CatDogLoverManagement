
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
        private readonly IAnimalRepository animalRepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public AddAnimal AddAnimalRequest { get; set; }

        [BindProperty]
        public IFormFile? FeaturedImage { get; set; }       
        public AddModel(IBlogPostRepository blogPostRepository, IAnimalRepository animalRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.animalRepository = animalRepository;
        }
        public void OnGet()
        {
           
        }

        public async Task<IActionResult> OnPost()
        {
          var userId = HttpContext.Session.GetString("userId");
            if(userId == null)
            {
                return BadRequest("You haven't logined");
            }

            if (ModelState.IsValid)
            {

             var result=   await blogPostRepository.AddAsync(userId, AddAnimalRequest, null, null, AddBlogPostRequest);
                if (result)
                {
                    TempData["success"] = "Create successfully";
                    return RedirectToPage("MySellOrGivePosts");
                }
                
                var notification = new Notification
                {
                    type = Repository.Models.Enums.NotificationType.Success,
                    Message = "New blog created!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                

            }



            return Page();
        }
    }
}

