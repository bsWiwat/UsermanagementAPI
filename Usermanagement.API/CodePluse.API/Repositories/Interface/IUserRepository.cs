using System;
using CodePluse.API.Models;

namespace CodePluse.API.Repositories.Interface
{
	public interface IUserRepository
	{
		Task<User> CreateAsync(User user);
       
        Task<IEnumerable<User>> GetAllAsync();

        Task<IEnumerable<User>> GetAllWithRelatedDataAsync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100);

        Task<User> GetByIdAsync(string id);

        Task<bool> DeleteByIdAsync(string id);

        Task<User?> UpdateAsync(User user);

        Task<int> GetCount();
    }
}

