using MongoDB.Driver;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Persistence.Collections;
using Nucleus.Core.Persistence.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nucleus.Core.Persistence.Services
{
    public static class Projections
    {
        public static Expression<Func<UserCollection, User>> Users => user => new User()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            EmailAddress = user.EmailAddress,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Active = user.Active,
            UserModules = user.UserModules == null ? null : user.UserModules.Select(module => new UserModule()
            {
                Code = module.Code,
                Name = module.Name,
                Roles = module.Roles == null ? null : module.Roles.Select(role => new Role()
                {
                    Code = role.Code,
                    Name = role.Name,
                    Rights = role.Rights == null ? null : role.Rights.Select(right => new PermissionBase()
                    {
                        Name = right.Name,
                        Code = right.Code
                    }).ToList()
                }).ToList()
            }).ToList(),
            AddressLine1 = user.AddressLine1,
            AddressLine2 = user.AddressLine2,
            PostalCode = user.PostalCode,
            Bio = user.Bio,
            City = user.City,
            Country = user.Country,
            StateProvince = user.StateProvince,
            PhoneNumber = user.PhoneNumber,
            Dob = user.Dob,
            NotificationSettings = user.NotificationSettings,
            CreatedOn = user.CreatedOn
        };
    }
}
