
using Eliassen.MongoDB.Extensions;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Core.Persistence.Collections;

[CollectionName("users")]
[BsonIgnoreExtraElements]
public class UserCollection
{
    [Key]
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? EmailAddress { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool Active { get; set; }
    public List<UserModuleCollection>? UserModules { get; set; }
    public DateTimeOffset? CreatedOn { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? Bio { get; set; }
    public string? City { get; set; }
    public CountryModel? Country { get; set; }
    public StateModel? StateProvince { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Dob { get; set; }
    public List<NotificationModel>? NotificationSettings { get; set; }
}