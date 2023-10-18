using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{

    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();


        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await catDogLoveManagementContext.BlogPosts.AddAsync(blogPost);
            await catDogLoveManagementContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBlog = await catDogLoveManagementContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                catDogLoveManagementContext.BlogPosts.Remove(existingBlog);
                await catDogLoveManagementContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await catDogLoveManagementContext.BlogPosts.ToListAsync();

        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await catDogLoveManagementContext.BlogPosts.FindAsync(id);
        }

        public Task<BlogPost> GetAsync(string image)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await catDogLoveManagementContext.BlogPosts.FindAsync(blogPost.PostId);

            if (existingBlogPost != null)
            {
                existingBlogPost.Title = blogPost.Title;
                existingBlogPost.Description = blogPost.Description;
                existingBlogPost.Price = blogPost.Price;
                existingBlogPost.CreatedDate = blogPost.CreatedDate;
                existingBlogPost.Status = blogPost.Status;
                existingBlogPost.Image = blogPost.Image;
            }

            await catDogLoveManagementContext.SaveChangesAsync();
            return existingBlogPost;
        }
    }
}
