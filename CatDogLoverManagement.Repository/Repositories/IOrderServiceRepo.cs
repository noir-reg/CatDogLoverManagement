using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IOrderServiceRepo
    {
        Task<IEnumerable<ViewOrderService>> GetAllOrderedAsync(string? buyerId, string? sellerId);
        Task<bool> AddAsync(string bookerId, string SellerId, string ServiceId, string timeFrameId);
        Task<bool> AddOrderServiceAsync(string bookerId, string sellerId, string serviceId, TimeFrame timeFrame);
        Task<bool> UpdateAsync(Order orderService);
        Task<bool> DeleteAsync(Guid id);
        Task<Order> GetByIdAsync(Guid id);
    }
}
