using System;
using CodePluse.API.Models;

namespace CodePluse.API.Repositories.Interface
{
	public interface IRoleRepository
	{
        Task<IEnumerable<Role>> GetAllAsync();
    }
}

