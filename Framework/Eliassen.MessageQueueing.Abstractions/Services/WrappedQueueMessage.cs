﻿namespace Eliassen.MessageQueueing.Services;

/// <summary>
/// Represents a wrapped queue message.
/// </summary>
public record WrappedQueueMessage : IQueueMessage
{
    /// <summary>
    /// Gets or initializes the content type of the message.
    /// </summary>
    public required string ContentType { get; init; }

    /// <summary>
    /// Gets or initializes the correlation ID of the message.
    /// </summary>
    public required string CorrelationId { get; init; }

    /// <summary>
    /// Gets or initializes the payload of the message.
    /// </summary>
    public required object Payload { get; init; }

    /// <summary>
    /// Gets or initializes the payload type of the message.
    /// </summary>
    public required string PayloadType { get; init; }

    /// <summary>
    /// Gets or initializes the properties associated with the message.
    /// </summary>
    public Dictionary<string, object?> Properties { get; init; }
}
