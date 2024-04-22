using System;
using CodePluse.API.Models;
using CodePluse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Repositories.Implementation
{
	public class PermissionRepository : IPermissionRepository
    {
        private readonly DBContext dbContext;

        public PermissionRepository(DBContext dbContext)
		{
			this.dbContext = dbContext;
		}

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await dbContext.Permissions.ToListAsync();
        }
    }
}

