﻿namespace Eliassen.OpenAI.AI.Services;

/// <summary>
/// Represents options for configuring the OpenAI service.
/// </summary>
public class OpenAIClientOptions
{
    /// <summary>
    /// Gets or sets the APIKey to be used.
    /// </summary>
    public required string APIKey { get; set; }

    /// <summary>
    /// Gets or sets the deployment model to be used for the text generation.
    /// </summary>
    public required string Model { get; set; }

    /// <summary>
    /// Gets or sets the deployment model to be used for the embedding generation.
    /// </summary>
    public required string EmbeddingModel { get; set; }

    /// <summary>
    /// Gets or sets the deployment model to be used for the embedding generation.
    /// </summary>
    public required string URL { get; set; }
}
