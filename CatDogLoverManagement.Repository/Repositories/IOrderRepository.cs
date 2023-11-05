using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderForSellOrGivePost(Guid animalId,string giverId, string recieverId,decimal price);
        

        
    }
}
