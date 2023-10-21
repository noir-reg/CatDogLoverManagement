using CatDogLoverManagement.Repository.Models;
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
        await catDogLoveManagementContext.AddAsync(new Comment
        {
            CommentId = Guid.NewGuid(),
            PostId = Guid.Parse(postId),
            UserId = Guid.Parse(volunteerId),
            CommentMessage = message,
            CreatedDate= DateTime.Now,
            Ischeck = false
        });
        var result=await catDogLoveManagementContext.SaveChangesAsync();
        if(result>0)
            return true;
        return false;

    }
}
