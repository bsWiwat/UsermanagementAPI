using System;
using CodePluse.API.Models;
using CodePluse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext dbContext;

        public UserRepository(DBContext context)
        {
            this.dbContext = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithRelatedDataAsync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100)

        {
            var users = dbContext.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                users = users.Where(u =>
                        u.FirstName.Contains(query) ||
                        u.LastName.Contains(query) ||
                        u.Email.Contains(query) ||
                        u.Role.RoleName.Contains(query) ||
                        u.UserPermissions.Any(up => up.Permission.PermissionName.Contains(query))
        );
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if(string.Equals(sortBy, "FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    users = isAsc ? users.OrderBy(u => u.FirstName) : users.OrderByDescending(u => u.FirstName);
                }

                if (string.Equals(sortBy, "LastName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    users = isAsc ? users.OrderBy(u => u.LastName) : users.OrderByDescending(u => u.LastName);
                }

                if (string.Equals(sortBy, "RoleName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    users = isAsc ? users.OrderBy(u => u.Role.RoleName) : users.OrderByDescending(u => u.Role.RoleName);
                }
            }

            //Pgagination
            //var query = dbContext.Users
            //    .Include(u => u.Role)
            //    .Include(u => u.UserPermissions)
            //        .ThenInclude(up => up.Permission);

            //Pgagination
            //int skipResults = (pageNumber - 1) * pageSize ?? 0;
            //skipResults = Math.Max(skipResults, 0);
            int skipResults = (pageNumber - 1) * pageSize ?? 0;
            skipResults = Math.Max(skipResults, 0);

            //users = users.Skip(skipResults ?? 0).Take(pageSize ?? 100);
            users = users.Skip(skipResults).Take(pageSize ?? 100);

            //Pgagination
            //var users = await query
            //    .Skip(skipResults)
            //    .Take(pageSize ?? 100)
            //    .ToListAsync();

            //normal result
            //return await dbContext.Users
            //        .Include(u => u.Role)
            //        .Include(u => u.UserPermissions)
            //            .ThenInclude(up => up.Permission)
            //        .ToListAsync();

            users = users.Include(u => u.Role)
                 .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission);

            return await users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            //return await dbContext.Users.FindAsync(id);
            return await dbContext.Users
                .Include(u => u.Role)
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var userToDelete = await dbContext.Users
                .Include(u => u.UserPermissions) // Include user permissions
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userToDelete == null)
            {
                return false; // User not found
            }

            dbContext.UserPermissions.RemoveRange(userToDelete.UserPermissions);

            dbContext.Users.Remove(userToDelete);
            await dbContext.SaveChangesAsync();

            return true; // User deleted successfully
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser != null)
            {
                dbContext.Entry(existingUser).CurrentValues.SetValues(user);
                await dbContext.SaveChangesAsync();
                return user;
            }

            return null;
        }

        public async Task<int> GetCount()
        {
            return await dbContext.Users.CountAsync();
        }
    }
}

