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
            new Dictionary<string, Expression<Func<User, object>>>(StringComparer.CurrentCultureIgnoreCase)
            {
                { FirstNameLastName , e => e.FirstName + " " + e.LastName },
                { LastNameFirstName , e => e.LastName + " " + e.FirstName},
            }.TryGetValue(key, out var exp) ? exp : null;

        public static Expression<Func<User, bool>>? PredicateMap(string key, object value) =>
            new Dictionary<string, Expression<Func<User, bool>>>(StringComparer.CurrentCultureIgnoreCase)
            {
                { Module , e => e.UserModules.Any(um => um.Code.Equals(value)) },
                { Role , e => e.UserModules.Any(um => um.Roles.Any(r => r.Code.Equals(value))) },
                { UserStatus, e => value.Equals("-1") || e.Active == value.Equals("1") },
            }.TryGetValue(key, out var exp) ? exp : null;

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set;}

        public string? Bio { get; set;}

        public string? City { get; set;}

        public CountryModel? Country { get; set;}

        public StateModel? StateProvince { get; set;}

        public string? PhoneNumber { get; set;}

        public DateTimeOffset? Dob { get; set;}

        public List<NotificationModel>? NotificationSettings { get; set;}
    }
}
