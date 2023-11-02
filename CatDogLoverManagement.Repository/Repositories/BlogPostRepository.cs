using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.Enums;
using CatDogLoverManagement.Repository.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{

    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();
        public async Task<bool> AddAsync(string id, AddAnimal animal, AddService service, AddTimeFrame timeFrame, AddBlogPost blogPost)
        {
            if (animal != null)
            {
                var animalModel = new Animal
                {
                    AnimalId = Guid.NewGuid(),

                    AnimalName = animal.AnimalName,

                    AnimalType = animal.AnimalType,
                    Description = animal.Description,
                    Gender = animal.Gender,
                    Age = animal.Age
                };
                await catDogLoveManagementContext.Animals.AddAsync(animalModel);
                var rs1 = await catDogLoveManagementContext.SaveChangesAsync();
                if (rs1 > 0)
                {

                    var animalId = animalModel.AnimalId;
                    await catDogLoveManagementContext.BlogPosts.AddAsync(new BlogPost
                    {
                        PostId = Guid.NewGuid(),
                        Price = blogPost.Price,
                        Title = blogPost.Title,
                        Description = animalModel.Description,
                        AnimalId = animalId,
                        UserId = Guid.Parse(id),
                        CreatedDate = DateTime.Now,
                        Status = BlogPostStatus.Processing.ToString(),
                        Image = blogPost.Image,
                    });
                    var check1 = await catDogLoveManagementContext.SaveChangesAsync();
                    if (check1 > 0)
                        return true;
                }
                return false;
            }
            if (service != null)
            {
                var serviceModel = new Service
                {
                    ServiceId = Guid.NewGuid(),
                    ServiceName = service.ServiceName,
                    Address = service.Address,
                    //   Price = (decimal)service.Price,
                    Description = service.Description,
                    CreatedDate = DateTime.Now,
                    OpenDate = service.OpenDate,
                    Status = "Ok",
                    Note = service.Note,
                    //  Image = service.Image,
                };
                await catDogLoveManagementContext.Services.AddAsync(serviceModel);
                var rs2 = await catDogLoveManagementContext.SaveChangesAsync();
                if (rs2 > 0)
                {
                    var servicelId = serviceModel.ServiceId;
                    var blogModel = new BlogPost
                    {
                        PostId = Guid.NewGuid(),
                        Price = blogPost.Price,
                        Title = blogPost.Title,
                        Description = serviceModel.Description,
                        ServiceId = servicelId,
                        UserId = Guid.Parse(id),
                        CreatedDate = DateTime.Now,
                        Status = BlogPostStatus.Processing.ToString(),
                        Image = blogPost.Image,
                    };
                    await catDogLoveManagementContext.BlogPosts.AddAsync(blogModel);
                    var check2 = await catDogLoveManagementContext.SaveChangesAsync();
                    if (check2 > 0)
                    {
                        var ser = await catDogLoveManagementContext.Services.Where(x => x.ServiceId == blogModel.ServiceId).FirstOrDefaultAsync();
                        ser.Price = blogModel.Price;
                        ser.Image = blogModel.Image;
                        await catDogLoveManagementContext.SaveChangesAsync();

                        await catDogLoveManagementContext.AddAsync(new TimeFrame
                        {
                            Id = Guid.NewGuid(),
                            ServiceId = serviceModel.ServiceId,
                            From = timeFrame.From,
                            To = timeFrame.To,
                        });
                        var result = await catDogLoveManagementContext.SaveChangesAsync();
                        if (result > 0)
                            return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingPost = await catDogLoveManagementContext.BlogPosts.FindAsync(id);

            if (existingPost != null)
            {
                existingPost.Status = BlogPostStatus.Unavailable.ToString();
            }
            await catDogLoveManagementContext.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<SellOrGivePostDTO>> GetAllSellPostAsync()
        {

            var result = await catDogLoveManagementContext.BlogPosts.Include(x => x.Animal).Where(x => x.Status == BlogPostStatus.Approved.ToString() && x.AnimalId != null).Select(x => new SellOrGivePostDTO
            {
                PostId = x.PostId,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                CreatedDate = x.CreatedDate,
                Image = x.Image,
                UserId = x.UserId,
                AnimalId = x.AnimalId,
                AnimalName = x.Animal.AnimalName,
                Gender = x.Animal.Gender,
                AnimalType = x.Animal.AnimalType,
                AnimalDescription = x.Animal.Description,
                Status = x.Status,
                Age = x.Animal.Age
            }).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ServicePostDTO>> GetAllServicePostAsync()
        {
            var result = await catDogLoveManagementContext.BlogPosts.Where(x => x.Status == BlogPostStatus.Approved.ToString() && x.ServiceId != null).Include(x => x.Service).ThenInclude(x => x.TimeFrames).Select(x => new ServicePostDTO
            {
                PostId = x.PostId,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                CreatedDate = x.CreatedDate,
                Image = x.Image,
                UserId = x.UserId,
                ServiceId = x.ServiceId,
                ServiceName = x.Service.ServiceName,
                Address = x.Service.Address,
                ServiceNote = x.Service.Description,
                OpenDate = x.Service.OpenDate,
                Status = x.Status
                ,
                Id = x.Service.TimeFrames.FirstOrDefault().Id,
                From = x.Service.TimeFrames.FirstOrDefault().From,
                To = x.Service.TimeFrames.FirstOrDefault().To
            }).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ServicePostDTO>> GetAllServicePostAsync(string id)
        {
            var result = await catDogLoveManagementContext.BlogPosts.Where(x => x.UserId != null && x.UserId == Guid.Parse(id) && !x.Status.Equals(BlogPostStatus.Unavailable.ToString()) && x.ServiceId != null).Include(x => x.Service).ThenInclude(x => x.TimeFrames).Select(x => new ServicePostDTO
            {
                PostId = x.PostId,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                CreatedDate = x.CreatedDate,
                Image = x.Image,
                UserId = x.UserId,
                ServiceId = x.ServiceId,
                ServiceName = x.Service.ServiceName,
                Address = x.Service.Address,
                ServiceNote = x.Service.Description,
                OpenDate = x.Service.OpenDate,
                Status = x.Status
                ,
                Id = x.Service.TimeFrames.FirstOrDefault().Id,
                From = x.Service.TimeFrames.FirstOrDefault().From,
                To = x.Service.TimeFrames.FirstOrDefault().To
            }).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SellOrGivePostDTO>> GetAllSellPostAsync(string id)
        {

            var result = await catDogLoveManagementContext.BlogPosts.Include(x => x.Animal).Where(x => x.UserId != null && x.UserId == Guid.Parse(id) && !x.Status.Equals(BlogPostStatus.Unavailable.ToString()) && x.AnimalId != null).Select(x => new SellOrGivePostDTO
            {
                PostId = x.PostId,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                CreatedDate = x.CreatedDate,
                Image = x.Image,
                UserId = x.UserId,
                AnimalId = x.AnimalId,
                AnimalName = x.Animal.AnimalName,
                Gender = x.Animal.Gender,
                AnimalType = x.Animal.AnimalType,
                AnimalDescription = x.Animal.Description,
                Status = x.Status,
                Age = x.Animal.Age
            }).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return result;
        }
        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await catDogLoveManagementContext.BlogPosts.Where(x => x.PostId == id).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateAsync(BlogPost blogPost)
        {
            var existingPost = await catDogLoveManagementContext.BlogPosts.FindAsync(blogPost.PostId);

            if (existingPost != null)
            {
                existingPost.Description = blogPost.Description;
                existingPost.Title = blogPost.Title;
                existingPost.Image = blogPost.Image;
                existingPost.Price = blogPost.Price;
                existingPost.Status = BlogPostStatus.Processing.ToString();
                var result = await catDogLoveManagementContext.SaveChangesAsync();
                if (result > 0)
                {


                    if (existingPost.ServiceId != null)
                    {
                        Service service = await catDogLoveManagementContext.Services.FindAsync(existingPost.ServiceId);
                        if (service != null)
                        {
                            service.Price = blogPost.Price;
                            var rs = await catDogLoveManagementContext.SaveChangesAsync();
                            if (rs > 0)
                            {
                                return true;
                            }
                        }
                    }



                }
            }

            return false;
        }

        public async Task<IEnumerable<BlogPost>> Get10NewestPostReviewAsync()
        {
            var result = await catDogLoveManagementContext.BlogPosts.OrderByDescending(x => x.CreatedDate).Take(10).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BlogPost>> GetAllGivePostAsync(string id)
        {
            var list = await catDogLoveManagementContext.BlogPosts
         .Where(x => x.Price == (Decimal)0
             && x.AnimalId != null
             && x.UserId == Guid.Parse(id)
             && x.Status.Equals(BlogPostStatus.Approved.ToString()))
         .ToListAsync();

            var postsWithApprovedComments = await catDogLoveManagementContext.Comments
                .Where(x => x.Ischeck)
                .Select(x => x.PostId)
                .ToListAsync();

            return list.Where(x => !postsWithApprovedComments.Contains(x.PostId));
        }

        public async Task<Guid> GetAnimalId(string id)
        {
            var animalId=(Guid) await catDogLoveManagementContext.BlogPosts.Where(x=>x.PostId==Guid.Parse(id)).Select(x=>x.AnimalId).FirstOrDefaultAsync();
            return animalId;
        }

        public async Task<bool> UpdateAdminAsync(BlogPost blogPost)
        {
            var existingPost = await catDogLoveManagementContext.BlogPosts.FindAsync(blogPost.PostId);

            if (existingPost != null)
            {
                existingPost.Description = blogPost.Description;
                existingPost.Title = blogPost.Title;
                existingPost.Image = blogPost.Image;
                existingPost.Price = blogPost.Price;
                existingPost.Status = blogPost.Status;
                var result = await catDogLoveManagementContext.SaveChangesAsync();
                if (result > 0)
                {
                    if (existingPost.ServiceId != null)
                    {
                        Service service = await catDogLoveManagementContext.Services.FindAsync(existingPost.ServiceId);
                        if (service != null)
                        {
                            service.Price = blogPost.Price;
                            var rs = await catDogLoveManagementContext.SaveChangesAsync();
                            if (rs > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await catDogLoveManagementContext.BlogPosts.ToListAsync();
        }
    }
}
