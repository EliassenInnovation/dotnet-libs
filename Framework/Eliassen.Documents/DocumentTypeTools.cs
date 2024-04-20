﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eliassen.Documents;

/// <summary>
/// Represents a toolset for managing document types.
/// </summary>
public class DocumentTypeTools : IDocumentTypeTools
{
    private readonly IEnumerable<IDocumentType> _types;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeTools"/> class with the specified document types.
    /// </summary>
    /// <param name="types">The collection of document types.</param>
    public DocumentTypeTools(
        IEnumerable<IDocumentType> types
        ) => _types = types;

    /// <summary>
    /// Retrieves the document type associated with the specified content type.
    /// </summary>
    /// <param name="contentType">The content type to search for.</param>
    /// <returns>The document type associated with the content type, if found; otherwise, null.</returns>
    public IDocumentType? GetByContentType(string contentType) =>
        _types.FirstOrDefault(t => t.ContentTypes.Any(i => string.Equals(i, contentType, StringComparison.OrdinalIgnoreCase)));

    /// <summary>
    /// Retrieves the document type associated with the specified file extension.
    /// </summary>
    /// <param name="fileExtension">The file extension to search for.</param>
    /// <returns>The document type associated with the file extension, if found; otherwise, null.</returns>
    public IDocumentType? GetByFileExtension(string fileExtension) =>
        _types.FirstOrDefault(t => t.FileExtensions.Any(i => string.Equals(i, fileExtension, StringComparison.OrdinalIgnoreCase)));

    /// <summary>
    /// Retrieves the document type associated with the specified file header.
    /// </summary>
    /// <param name="stream">The stream containing the file header.</param>
    /// <returns>The document type associated with the file header, if found; otherwise, null.</returns>
    public IDocumentType? GetByFileHeader(Stream stream)
    {
        var maxRead = _types.Max(t => t.FileHeader.Length);
        if (maxRead > 0)
        {
            var p = stream.Position;

            Span<byte> temp = new byte[maxRead];
            var possible = _types.Where(t => t.FileHeader.Length > 0);
            stream.Read(temp);

            foreach (var t in possible.Where(t => t.FileHeader.Length > 0))
                if (temp.StartsWith(t.FileHeader))
                    return t;

            stream.Position = 0;
        }

        return default;
    }
}
