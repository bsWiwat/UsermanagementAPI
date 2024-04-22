using System;
using System.Collections.Generic;

namespace CodePluse.API.Models;

public partial class Permission
{
    public string PermissionId { get; set; } = null!;

    public string PermissionName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
