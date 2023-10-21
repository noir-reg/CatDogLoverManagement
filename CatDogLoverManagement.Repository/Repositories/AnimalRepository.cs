using CatDogLoverManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public class AnimalRepository:IAnimalRepository
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();
        public async Task<Animal> AddAsync(Animal animal)
        {
            await catDogLoveManagementContext.Animals.AddAsync(animal);
            await catDogLoveManagementContext.SaveChangesAsync();
            return animal;
        }
        public async Task<Animal> GetAsync(Guid id)
        {
            return await catDogLoveManagementContext.Animals.FindAsync(id);
        }
        public async Task<bool> UpdateAsync(Animal animal)
        {
            var existingAnimal = await catDogLoveManagementContext.Animals.FindAsync(animal.AnimalId);

            if (existingAnimal != null)
            {
                existingAnimal.Age = animal.Age;
                existingAnimal.AnimalName = animal.AnimalName;
                existingAnimal.AnimalType = animal.AnimalType;
                existingAnimal.Description = animal.Description;
                existingAnimal.Gender = animal.Gender;
                
            }
          var result =  await catDogLoveManagementContext.SaveChangesAsync();
            if(result>0)
                return true;
            return false;
        }

       
    }
}
