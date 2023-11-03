
using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class AddServiceModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        private readonly IServiceRepository serviceRepository;
        private readonly ITimeFrameRepository timeFrameRepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public AddService AddServiceRequest { get; set; }

        [BindProperty]
        public List<AddTimeFrame> AddTimeFrameRequest { get; set; }

        [BindProperty]
        public IFormFile? FeaturedImage { get; set; }
        public AddServiceModel(IBlogPostRepository blogPostRepository, IServiceRepository serviceRepository, ITimeFrameRepository timeFrameRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.serviceRepository = serviceRepository;
            this.timeFrameRepository = timeFrameRepository;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return BadRequest("You haven't logined");
            }


            if (ModelState.IsValid)
            {
                if (AddTimeFrameRequest.Count > 0)
                {
                    var result = await blogPostRepository.AddServiceContainListTimeAsync(userId,AddServiceRequest, AddTimeFrameRequest, AddBlogPostRequest);
                    if (result)
                    {
                        return RedirectToPage("MyServicePosts");
                    }

                    var notification = new Notification
                    {
                        type = Repository.Models.Enums.NotificationType.Success,
                        Message = "New blog created!"
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);

                }
            }
            return Page();
        }
    }
}

