using System;
using CodePluse.API.Models;

namespace CodePluse.API.Repositories.Interface
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
    }
}

