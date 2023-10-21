using CatDogLoverManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IAnimalRepository
    {      
        Task<Animal> GetAsync(Guid id);      
        Task<Animal> AddAsync(Animal animal);
        Task<bool> UpdateAsync(Animal animal);      
    }
}
