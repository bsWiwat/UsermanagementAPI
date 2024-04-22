using System;
namespace CodePluse.API.DTO
{
	public class GetUserDTO
	{
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public string RoleId { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string PermissionId { get; set; } = null!;

        public string PermissionName { get; set; } = null!;
    }
}

