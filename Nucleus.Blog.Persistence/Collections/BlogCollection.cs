﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nucleus.Blog.Persistence.Collections;

public class BlogCollection
{
    [Key]
    public string? BlogId { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public List<string?>? Tags { get; set; }
    public string? Preview { get; set; }
    public string? Content { get; set; }
    public bool? Enabled { get; set; }
    public string? Author { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}
