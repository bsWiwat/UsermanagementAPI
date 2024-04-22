using System;
namespace CodePluse.API.DTO
{
	public class UpdateUserDTO
	{
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string RoleId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public List<UserPermissionDTO> UserPermissions { get; set; } = new List<UserPermissionDTO>();
    }
}

