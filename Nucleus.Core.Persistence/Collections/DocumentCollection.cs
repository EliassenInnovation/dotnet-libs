using Eliassen.MongoDB.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Core.Persistence.Collections;

[CollectionName("documents")]
public class DocumentCollection
{
    [Key]
    public string? DocumentId { get; set; }

    public string? DocumentKey { get; set; }
    public string? DocumentName { get; set; }
    public string? DocumentType { get; set; }
    public int? documentSize { get; set; }
    public string? DocumentRepository { get; set; }
    public string? DocumentCategory { get; set; }
    public DateTimeOffset? CreatedOn { get; set; }
}
