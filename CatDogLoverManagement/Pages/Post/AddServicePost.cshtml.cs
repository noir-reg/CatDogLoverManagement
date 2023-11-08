
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

            ValidateAddService();

           
                if (AddTimeFrameRequest.Count > 0)
                {
                    List<AddTimeFrame> timeFramesTemp = new List<AddTimeFrame>();
                    foreach (var timeFrame in AddTimeFrameRequest)
                    {
                        if (timeFrame.From != null && timeFrame.To != null)
                        {
                            timeFramesTemp.Add(timeFrame);
                        }
                    }
                    if (timeFramesTemp.Count > 0)
                    {
                        var result = await blogPostRepository.AddServiceContainListTimeAsync(userId, AddServiceRequest, timeFramesTemp, AddBlogPostRequest);
                        if (result)
                        {
                            TempData["success"] = "Create service successfully";
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

        private void ValidateAddService()
        {
            if (AddServiceRequest.OpenDate.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("AddServiceRequest.OpenDate",
                    $"OpenDate can only be today's date or a furture date.");
            }
        }
    }
}

