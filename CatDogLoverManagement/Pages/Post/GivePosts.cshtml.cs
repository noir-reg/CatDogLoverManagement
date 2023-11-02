using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatDogLoverManagement.Pages.Post
{
    public class GivePostsModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;
        
        public GivePostsModel(IBlogPostRepository blogPostRepository )
        {
            this.blogPostRepository = blogPostRepository;
            
        }
        public IEnumerable<BlogPost> BlogPosts { get; set; }
       

        public async Task OnGet()
        {
            string id = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(id))
            {
                BlogPosts = await blogPostRepository.GetAllGivePostAsync(id);
            }
        }

        
    }
}
