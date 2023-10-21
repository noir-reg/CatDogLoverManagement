using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CatDogLoverManagement.Pages.Post
{
    public class MySellList : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        public IEnumerable<SellOrGivePostDTO> BlogPosts { get; set; }
        public MySellList(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task OnGet()
        {
            var id= HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(id)) {
                BlogPosts = await blogPostRepository.GetAllSellPostAsync(id);
            }
            
        }
    }
}
