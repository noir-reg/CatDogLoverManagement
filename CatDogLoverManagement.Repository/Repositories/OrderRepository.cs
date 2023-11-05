using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();
        public async Task<bool> CreateOrderForSellOrGivePost(Guid animalId, string giverId, string recieverId,decimal price)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                BuyerId = Guid.Parse(recieverId),
                SellerId = Guid.Parse(giverId),
                OrderDate = DateTime.Now,
                BookedDate = null,
                BookedTime = null,
                AnimalId = animalId,
                ServiceId = null,
                Price = price,
                Status = price>0? "Waiting":"Success"
            };
            await catDogLoveManagementContext.Orders.AddAsync(order);
            var result = await catDogLoveManagementContext.SaveChangesAsync();
            if (result > 0)
            {
                await catDogLoveManagementContext.Transactions.AddAsync(new Transaction
                {
                    OrderId = order.OrderId
                });
              await  catDogLoveManagementContext.SaveChangesAsync();
                
                return true;
            }
            return false;
        }
    }
}
