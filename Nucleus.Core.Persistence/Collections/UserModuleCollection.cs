using System.Collections.Generic;

namespace Nucleus.Core.Persistence.Collections;

public class UserModuleCollection : PermissionBaseCollection
{
    public List<RoleCollection>? Roles { get; set; }
}