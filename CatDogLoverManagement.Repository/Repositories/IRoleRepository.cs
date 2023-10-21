using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public interface IRoleRepository
    {
        Task<Guid> GetRoleId(string roleName);
    }
}
