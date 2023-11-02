using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task<User> LoginAsync(string username, string password);
        Task<AccountDTO> GetAccount(string username, string password);
    }
}
