using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodePluse.API.Models;

public partial class UserPermission
{
    public string UserId { get; set; } = null!;

    public string PermissionId { get; set; } = null!;

    public bool? IsReadable { get; set; }

    public bool? IsWritable { get; set; }

    public bool? IsDeleteable { get; set; }

    public bool? IsActive { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
