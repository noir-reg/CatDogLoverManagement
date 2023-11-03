using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public class OrderServiceRepo : IOrderServiceRepo
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();

        public Task<bool> AddAsync(string bookerId, string SellerId, string ServiceId, string timeFrameId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddOrderServiceAsync(string bookerId, string sellerId, string serviceId, TimeFrame timeFrame)
        {
            if (timeFrame.From != null && timeFrame.To != null && bookerId != null && sellerId != null && serviceId != null)
            {
                var service = await catDogLoveManagementContext.Services.FirstOrDefaultAsync(s => s.ServiceId.Equals(Guid.Parse(serviceId)));
                var orderServiceModel = new Order
                {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    BookedDate = service.OpenDate,
                    BookedTime = timeFrame.From + ";" + timeFrame.To,
                    Status = "Success",
                    Price = (decimal)(service.Price != null ? service.Price : 0),
                    BuyerId = Guid.Parse(bookerId),
                    SellerId = Guid.Parse(sellerId),
                    ServiceId = service.ServiceId,
                };
                await catDogLoveManagementContext.Orders.AddAsync(orderServiceModel);
                var check1 = await catDogLoveManagementContext.SaveChangesAsync();
                if (check1 > 0)
                    return true;
            }
            return false;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ViewOrderService>> GetAllOrderedAsync(string? buyerId, string? sellerId)
        {
            IEnumerable<Order> list = null;
            if (buyerId != null)
            {
                list = await catDogLoveManagementContext.Orders.Where(b => b.BuyerId.Equals(Guid.Parse(buyerId))).ToListAsync();
            }

            if (sellerId != null)
            {
                list = await catDogLoveManagementContext.Orders.Where(b => b.SellerId.Equals(Guid.Parse(sellerId))).ToListAsync();
            }
            IEnumerable<ViewOrderService> viewList = null;
            if (list != null)
            {
                viewList = await ConvertOrderToViewOrderService(list);
            }
            return viewList;
        }

        private async Task<ICollection<ViewOrderService>> ConvertOrderToViewOrderService(IEnumerable<Order> list)
        {
            ICollection<ViewOrderService> viewList = new Collection<ViewOrderService>();
            ViewOrderService viewOrderService = null;
            foreach (var order in list)
            {
                if (order != null)
                {
                    string[] bookTime = order.BookedTime!.Split(';');
                    if (bookTime.Length >= 2 && order.ServiceId != null)
                    {
                        BlogPost blogPost = await catDogLoveManagementContext.BlogPosts.FirstOrDefaultAsync(b => b.ServiceId == order.ServiceId);
                        Service service = await catDogLoveManagementContext.Services.FirstOrDefaultAsync(s => s.ServiceId == order.ServiceId);
                        viewOrderService = new()
                        {
                            OrderId = order.OrderId,
                            BuyerName = (await catDogLoveManagementContext.Users.FirstOrDefaultAsync(c => c.UserId == order.BuyerId)).Username,
                            SellerName = (await catDogLoveManagementContext.Users.FirstOrDefaultAsync(c => c.UserId == order.SellerId)).Username,
                            OrderDate = order.OrderDate,
                            BookedDate = order.BookedDate,
                            From = TimeSpan.Parse(bookTime[0]),
                            To = TimeSpan.Parse(bookTime[1]),
                            Status = order.Status,
                            Price = order.Price,
                            Image = blogPost!.Image,
                            Title = blogPost.Title,
                            ServiceModel = new ServiceModel()
                            {
                                ServiceId = service!.ServiceId,
                                ServiceName = service.ServiceName,
                                Address = service.Address,
                                Price = (decimal)service.Price!,
                                Description = service.Description,
                                CreatedDate = service.CreatedDate,
                                OpenDate = service.OpenDate,
                                Status = service.Status,
                                Note = service.Note,
                                Image = service.Image!
                            }
                        };
                        viewList.Add(viewOrderService);
                    } else if(order.AnimalId != null)
                    {
                        BlogPost? blogPost = await catDogLoveManagementContext.BlogPosts.FirstOrDefaultAsync(b => b.AnimalId == order.AnimalId);
                        Animal? animal = await catDogLoveManagementContext.Animals.FirstOrDefaultAsync(s => s.AnimalId == order.AnimalId);
                        viewOrderService = new()
                        {
                            OrderId = order.OrderId,
                            BuyerName = (await catDogLoveManagementContext.Users.FirstOrDefaultAsync(c => c.UserId == order.BuyerId)).Username,
                            SellerName = (await catDogLoveManagementContext.Users.FirstOrDefaultAsync(c => c.UserId == order.SellerId)).Username,
                            OrderDate = order.OrderDate,
                            BookedDate = order.BookedDate,
                            From = TimeSpan.Parse(bookTime[0]),
                            To = TimeSpan.Parse(bookTime[1]),
                            Status = order.Status,
                            Price = order.Price,
                            Image = blogPost!.Image,
                            Title = blogPost.Title,
                            PostModel = new ViewSellOrGivePost()
                            {
                                AnimalId = animal!.AnimalId,
                                AnimalName = animal.AnimalName,
                                AnimalType = animal.AnimalType,
                                Gender = animal.Gender,
                                Age = animal.Age,
                            }
                        };
                        viewList.Add(viewOrderService);
                    }
                }
            }
            return viewList;
        }

        public async Task<bool> UpdateAsync(Order orderService)
        {
            catDogLoveManagementContext.Orders.Update(orderService);
            return catDogLoveManagementContext.SaveChanges() > 0;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            if (id == null) return null;
            return await catDogLoveManagementContext.Orders.FirstOrDefaultAsync(c => c.OrderId == id);
        }
    }
}
