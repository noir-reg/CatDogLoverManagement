using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;

namespace CatDogLoverManagement.Pages.Post
{
    public interface ITimeFrameRepository
    {
        Task<TimeFrame> GetAsync(Guid id);
        Task<TimeFrame> GetTimeFrameByIdAsync(Guid id);
        
        Task<bool> UpdateAsync(TimeFrame time);
        Task<bool> UpdateRangeAync(List<TimeFrame> listTime);
        Task<List<TimeFrame>> GetListTimeFrameByServiceId(Guid serviceId); 

    }
}