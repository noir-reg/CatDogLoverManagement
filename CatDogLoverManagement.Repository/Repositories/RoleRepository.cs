using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CatDogLoveManagementContext catDogLoveManagementContext = new();
        public async Task<Guid> GetRoleId(string roleName)
        {
            var id=await catDogLoveManagementContext.UserRoles.Where(x=>x.RoleName.Equals(roleName)).Select(x=>x.RoleId).FirstOrDefaultAsync();
            return id;
        }
    }
}
