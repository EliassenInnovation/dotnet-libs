using Eliassen.MongoDB.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Project.Persistence.Collections;

[CollectionName("projects")]
public class ProjectCollection
{
    [Key]
    public string? ProjectId { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? ProjectLink { get; set; }
    public string? ProjectImage { get; set; }
    public string? Preview { get; set; }
    public string? Content { get; set; }
    public string? Page { get; set; }
    public bool? Enabled { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}
