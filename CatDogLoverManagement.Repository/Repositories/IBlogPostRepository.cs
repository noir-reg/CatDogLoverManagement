using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> Get10NewestPostReviewAsync();
        Task<BlogPost> GetAsync(Guid id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<IEnumerable<ServicePostDTO>> GetAllServicePostAsync();
        Task<IEnumerable<ServicePostDTO>> GetAllServicePostAsync(string id);
        Task<IEnumerable<SellOrGivePostDTO>> GetAllSellPostAsync(string id);
        Task<IEnumerable<SellOrGivePostDTO>> GetAllSellPostAsync();
        Task<bool> AddAsync(string id, AddAnimal animal, AddService service, AddTimeFrame timeFrame, AddBlogPost blogPost);
        Task<bool> UpdateAsync(BlogPost blogPost);
        Task<bool> UpdateAdminAsync(BlogPost blogPost);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<BlogPost>> GetAllGivePostAsync(string id);
        Task<Guid> GetAnimalId(string id);
    }
}
