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
        public EditBlogPostRequest BlogPost { get; set; }
        [BindProperty]
        public EditService Service { get; set; }
        [BindProperty]
        public List<TimeFrame> AddTimeFrameRequest { get; set; }
        [BindProperty]
        public IFormFile? FeaturedImage { get; set; }
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
            var blogPostDomainModel = await blogPostRepository.GetAsync(id);
            if (blogPostDomainModel.ServiceId != null)
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

                if(blogPostDomainModel.ServiceId != null)
                {
                    var blogServicePostDomainModel = await serviceRepository.GetAsync((Guid)BlogPost.ServiceId);
                    Service = new EditService
                    {
                        ServiceId = blogServicePostDomainModel.ServiceId,
                        ServiceName = blogServicePostDomainModel.ServiceName,
                        Address = blogServicePostDomainModel.Address,
                        Description = blogServicePostDomainModel.Description,
                        OpenDate = blogServicePostDomainModel.OpenDate,
                        Note = blogServicePostDomainModel.Note,
                        Image = blogServicePostDomainModel.Image,

                    };

                    if(blogServicePostDomainModel != null)
                    {
                        AddTimeFrameRequest = await timeFrameRepository.GetListTimeFrameByServiceId(Service.ServiceId);
                    }
                }
            }
                
            
        }

        public async Task<IActionResult> OnPostEdit()
        {
            var check = await ValidateAddService();
           if (check)
            {
                try
                {
                    var blogPostDomainModel = new BlogPost
                    {
                        PostId = BlogPost.PostId,
                        Title = BlogPost.Title,
                        Description = BlogPost.Description,
                        Price = BlogPost.Price,
                        CreatedDate = BlogPost.CreatedDate,
                        Status = BlogPost.Status,
                        Image = BlogPost.Image,
                        UserId = BlogPost.UserId,
                        ServiceId = BlogPost.ServiceId,
                        AnimalId = BlogPost.AnimalId,
                    };

                    var blogService = new Service
                    {
                        ServiceId = Service.ServiceId,
                        ServiceName = Service.ServiceName,
                        Address = Service.Address,
                        Description = Service.Description,
                        OpenDate = Service.OpenDate,
                        Note = Service.Note,
                        Image = Service.Image,
                    };

                    List<TimeFrame> listTimeDTO = new List<TimeFrame>();
                    if (AddTimeFrameRequest.Count > 0)
                    {
                        foreach (var timeFrame in AddTimeFrameRequest)
                        {
                            if (timeFrame.From != null && timeFrame.To != null)
                            {
                                listTimeDTO.Add(timeFrame);
                            }
                        }
                    }
                    if (listTimeDTO.Count == 0)
                    {
                        TempData["error"] = "Please choose time";
                        return Page();
                    }
                    await blogPostRepository.UpdateAsync(blogPostDomainModel);
                    await timeFrameRepository.UpdateRangeAync(listTimeDTO);
                    var result = await serviceRepository.UpdateAsync(blogService);
                    if (result)
                    {
                        TempData["success"] = "Update service successfully";
                    }
                    ViewData["Notification"] = new Notification
                    {
                        Message = "Record updated successfully!",
                        type = Repository.Models.Enums.NotificationType.Success
                    };
                }
                catch (Exception ex)
                {
                    ViewData["Notification"] = new Notification
                    {
                        Message = "Something went wrong!",
                        type = Repository.Models.Enums.NotificationType.Error
                    };
                }
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

                return RedirectToPage("MyServicePosts");
            }

            return Page();
        }

        private async Task<bool> ValidateAddService()
        {   var result = true;
            var blogServicePostDomainModel = await serviceRepository.GetAsync(Service.ServiceId);

            var date = blogServicePostDomainModel.OpenDate.Date;
            if (Service.OpenDate.Date < date)
            {
                ModelState.AddModelError("Service.OpenDate",
                    $"New open date can not be earlier than current open date.");
                result = false;  
            }
            return result;
        }
    }
}
