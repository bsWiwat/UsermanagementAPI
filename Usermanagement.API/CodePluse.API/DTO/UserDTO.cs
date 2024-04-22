using System;
using System.Collections.Generic;

namespace CodePluse.API.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string RoleId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public List<UserPermissionDTO> UserPermissions { get; set; } = new List<UserPermissionDTO>();
    }

    public class UserPermissionDTO
    {
        public string PermissionId { get; set; } = null!;

        public bool IsReadable { get; set; }

        public bool IsWritable { get; set; }

        public bool IsDeletable { get; set; }

        //public string UserId { get; set; }
    }
}
