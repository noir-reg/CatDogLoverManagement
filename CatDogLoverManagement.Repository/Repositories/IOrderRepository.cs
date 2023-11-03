using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderForGivePost(Guid animalId,string giverId, string recieverId);
        

        
    }
}
