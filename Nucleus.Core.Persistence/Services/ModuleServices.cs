﻿using MongoDB.Driver;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Persistence;
using System.Linq;

namespace Nucleus.Core.Persistence.Services
{
    public class ModuleServices : IModuleService
    {
        private readonly ICoreMongoDatabase _db;
        public ModuleServices(
            ICoreMongoDatabase db
            )
        {
            _db = db;
        }

        public IQueryable<Module> Query() =>
            from module in _db.Modules.AsQueryable()
            select new Module()
            {
                ModuleId = module.ModuleId,
                Roles = module.Roles == null ? null : module.Roles.Select(role => new Role()
                {
                    Code = role.Code,
                    Name = role.Name,
                    Rights = role.Rights == null ? null : role.Rights.Select(right => new PermissionBase()
                    {
                        Name = right.Name,
                        Code = right.Code
                    }).ToList()
                }).ToList(),
                Name = module.Name,
                Code = module.Code
            };
    }
}
