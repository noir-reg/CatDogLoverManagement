using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatDogLoverManagement.Pages.Post
{
    public class MyServicePostModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        public IEnumerable<ServicePostDTO> BlogPosts { get; set; }
        public MyServicePostModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task OnGet()
        {
            var id = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(id))
                BlogPosts = await blogPostRepository.GetAllServicePostAsync(id);
        }
    }
}
