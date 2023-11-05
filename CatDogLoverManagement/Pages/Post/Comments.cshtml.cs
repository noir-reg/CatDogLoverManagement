using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;

namespace CatDogLoverManagement.Pages.Post
{
    public class CommentsModel : PageModel
    {
        private readonly ICommentRepository commentRepository;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IOrderRepository orderRepository;
        public CommentsModel(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository, IOrderRepository orderRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.commentRepository = commentRepository;
            this.orderRepository = orderRepository;
        }

        public IEnumerable<Comment> Comments { get; set; }


        public async Task OnGet(string id)
        {

            Comments = await commentRepository.GetAllGivePostComment(id);

        }
        public async Task<IActionResult> OnPost(string UserId, string PostId, string CommentId)
        {
            var id = HttpContext.Session.GetString("userId");

            if (!string.IsNullOrEmpty(id))
            {
                var AnimalId = await blogPostRepository.GetAnimalId(PostId);
                var result = await orderRepository.CreateOrderForSellOrGivePost(AnimalId, id, UserId,0);
                if (result)
                    await commentRepository.UpdateCommentStatus(CommentId);
                TempData["success"] = "Completed";
            }

            return RedirectToPage("GivePosts");

        }
    }
}
