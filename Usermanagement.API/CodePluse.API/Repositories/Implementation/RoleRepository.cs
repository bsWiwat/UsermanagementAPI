using System.Collections.Generic;
using System.Threading.Tasks;
using CodePluse.API.Models;
using CodePluse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DBContext dbContext;

        public RoleRepository(DBContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await dbContext.Roles.ToListAsync();
        }
    }
}