using CatDogLoverManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories;

public interface ICommentRepository
{
    Task<bool> AddAsync(string message,string postId,string volunteerId);
}

