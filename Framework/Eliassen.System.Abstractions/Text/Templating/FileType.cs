﻿namespace Eliassen.System.Text.Templating;

/// <summary>
/// Represents a file type, providing information about the file extension, content type, and whether it is a template type.
/// </summary>
public record FileType : IFileType
{
    /// <summary>
    /// Gets or sets the file extension associated with the file type.
    /// </summary>
    public string Extension { get; set; } = null!;

    /// <summary>
    /// Gets or sets the content type associated with the file type.
    /// </summary>
    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether the file type is a template type.
    /// </summary>
    public bool IsTemplateType { get; set; }
}
