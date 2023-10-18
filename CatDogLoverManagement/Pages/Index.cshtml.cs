using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatDogLoverManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPostRepository blogPostRepository;

        
        public List<BlogPost> Blogs { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBlogPostRepository blogPostRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> OnGet()
        {
           Blogs = (await blogPostRepository.GetAllAsync()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return Page();
        }
    }
}