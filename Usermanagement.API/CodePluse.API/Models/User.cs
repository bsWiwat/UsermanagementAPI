using System;
using System.Collections.Generic;

namespace CodePluse.API.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

    //public ICollection<Role> Roles { get; set; }  // add after updatdb
}
