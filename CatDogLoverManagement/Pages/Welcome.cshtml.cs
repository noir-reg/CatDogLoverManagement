using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatDogLoverManagement.Pages
{
    public class WelcomeModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;
        public string Username;

        public List<BlogPost> Blogs { get; set; }
        public WelcomeModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("username");
        }
        public async Task<IActionResult> OnGetBlog ()
        {
            Blogs = (await blogPostRepository.GetAllAsync()).ToList();
            return Page();
        }
    }
}
