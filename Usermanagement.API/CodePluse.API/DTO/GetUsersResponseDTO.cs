using System.Collections.Generic;
using static CodePluse.API.DTO.Reponse.ResponseDTO;

namespace CodePluse.API.DTO
{
    public class GetAllUsersResponseDTO
    {
        public Status Status { get; set; }

        public List<UserData> Data { get; set; }
    }

    public class UserData
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public RoleData Role { get; set; }

        public string Username { get; set; }

        public DateTime Date { get; set; }

        public List<PermissionData> Permissions { get; set; }
    }

    public class RoleData
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
    }

    public class PermissionData
    {
        public string PermissionId { get; set; }

        public string PermissionName { get; set; }
        public bool IsReadable { get; set; }
        public bool IsWritable { get; set; }
        public bool IsDeleteable { get; set; }
    }
}
