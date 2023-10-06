using Eliassen.System.ComponentModel.Search;
using Eliassen.System.Linq.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Nucleus.Core.Contracts.Models
{
    [Searchable(FirstNameLastName)]
    [Searchable(LastNameFirstName)]
    [Filterable(Module)]
    [Filterable(Role)]
    [Filterable(UserStatus)]
    [SearchTermDefault(SearchTermDefaults.StartsWith)]
    [DebuggerDisplay("{FirstName} {LastName}")]
    public class User
    {
        public const string FirstNameLastName = nameof(FirstNameLastName);
        public const string LastNameFirstName = nameof(LastNameFirstName);
        public const string Module = nameof(Module);
        public const string Role = nameof(Role);
        public const string UserStatus = nameof(UserStatus);

        [IgnoreStringComparisonReplacement]
        public string? UserId { get; set; }
        public string? UserName { get; set; }

        [Searchable]
        [DefaultSort(priority: 2, order: OrderDirections.Ascending)]
        public string? EmailAddress { get; set; }

        [Searchable]
        [DefaultSort(priority: 1, order: OrderDirections.Ascending)]
        public string? FirstName { get; set; }

        [Searchable]
        [DefaultSort(priority: 0, order: OrderDirections.Ascending)]
        public string? LastName { get; set; }

        public bool Active { get; set; }

        public List<UserModule>? UserModules { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public static Expression<Func<User, object>>? PropertyMap(string key) =>
            key switch
            {
                FirstNameLastName => e => e.FirstName + " " + e.LastName,
                LastNameFirstName => e => e.LastName + " " + e.FirstName,
                _ => null
            };

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public static Expression<Func<User, bool>>? PredicateMap(string key, object value) =>
            key switch
            {
                Module => e => e.UserModules.Any(um => um.Code.Equals(value)),
                Role => e => e.UserModules.Any(um => um.Roles.Any(r=>r.Code.Equals(value))),
                UserStatus => e => value.Equals("-1") || e.Active == value.Equals("1"),
                _ => null
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
