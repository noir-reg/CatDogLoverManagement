using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();
        public Task<Service> AddAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public async Task<Service> GetAsync(Guid id)
        {
            return await catDogLoveManagementContext.Services.FirstOrDefaultAsync(c => c.ServiceId == id);
        }

        public async Task<bool> UpdateAsync(Service service)
        {

            var existingService = await catDogLoveManagementContext.Services.FindAsync(service.ServiceId);

            if (existingService != null)
            {
                existingService.ServiceName = service.ServiceName;
                existingService.Address = service.Address;
                existingService.Description = service.Description;
                existingService.OpenDate = service.OpenDate;
                existingService .Note = service.Note;
            }
            var result = await catDogLoveManagementContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }
    }
}
