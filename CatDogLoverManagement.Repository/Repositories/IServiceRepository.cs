using CatDogLoverManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IServiceRepository
    {
        Task<Service> GetAsync(Guid id);
        Task<Service> AddAsync(Service service);
        Task<bool> UpdateAsync(Service service);
    }
}
