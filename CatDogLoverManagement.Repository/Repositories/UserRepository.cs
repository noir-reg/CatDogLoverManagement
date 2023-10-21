using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();
        public async Task<User> AddAsync(User user)
        {
            var check = await catDogLoveManagementContext.Users.Where(x => x.Username == user.Username).FirstOrDefaultAsync();
            if (check==null)
            {
                await catDogLoveManagementContext.Users.AddAsync(user);
                await catDogLoveManagementContext.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await catDogLoveManagementContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
