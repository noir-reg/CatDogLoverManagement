using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public async Task<AccountDTO> GetAccount(string username, string password)
        {
            var user = await catDogLoveManagementContext.Users.Where(x => x.Username.Equals(username) && x.Password == password).FirstOrDefaultAsync();
            if (user == null)
            {
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                var adminUsername = config["AdminAccount:userName"];
                var adminPassword = config["AdminAccount:passWord"];
                
                if(adminUsername.Equals(username) && adminPassword.Equals(password))
                {
                    return new AccountDTO { Username = adminUsername, Password = adminPassword };
                }
                return null;
            }
            return new AccountDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Phonenumber = user.Phonenumber,
                RoleId = user.RoleId,
            };
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await catDogLoveManagementContext.Users.Include(c => c.Role).FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        
    }
}
