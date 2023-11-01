using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories;

public class CommentRepository : ICommentRepository

{
    private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();
    public async Task<bool> AddAsync(string message, string postId, string volunteerId)
    {
        catDogLoveManagementContext.Comments.Add(new Comment
        {
            CommentId = Guid.NewGuid(),
            PostId = Guid.Parse(postId),
            UserId = Guid.Parse(volunteerId),
            CommentMessage = message,
            CreatedDate = DateTime.Now,
            Ischeck = false,


        });
        var result = await catDogLoveManagementContext.SaveChangesAsync();
        if (result > 0)
            return true;
        return false;

    }

    public async Task<IEnumerable<Comment>> GetAllGivePostComment(string id)
    {
        var list = await catDogLoveManagementContext.Comments.Where(x => x.PostId == Guid.Parse(id)&&!x.Ischeck).ToListAsync();
        return list;
    }

    public async Task<bool> UpdateCommentStatus(string id)
    {
        var comment = await catDogLoveManagementContext.Comments.Where(x => x.CommentId == Guid.Parse(id)).FirstOrDefaultAsync();
        if (comment != null)
        {
            comment.Ischeck = true;
            var result = await catDogLoveManagementContext.SaveChangesAsync();
            if (result > 0)
                return true;
        }

        return false;
    }
}
