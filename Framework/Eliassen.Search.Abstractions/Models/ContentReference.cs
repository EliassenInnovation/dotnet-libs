﻿namespace Eliassen.Search.Models;

public record ContentReference
{
    public Stream Content { get; init; }
    public string ContentType { get; init; }
    public string FileName { get; init; }
}
