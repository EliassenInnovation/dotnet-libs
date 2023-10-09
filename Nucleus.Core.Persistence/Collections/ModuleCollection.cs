using Eliassen.MongoDB.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Core.Persistence.Collections;

[CollectionName("modules")]
public class ModuleCollection : PermissionBaseCollection
{
    [Key]
    public string? ModuleId { get; set; }

    public List<RoleCollection>? Roles { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}
