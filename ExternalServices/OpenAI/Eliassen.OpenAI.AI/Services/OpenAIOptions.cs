﻿namespace Eliassen.OpenAI.AI.Services;

/// <summary>
/// Represents options for configuring the OpenAI service.
/// </summary>
public class OpenAIOptions
{
    /// <summary>
    /// Gets or sets the APIKey to be used.
    /// </summary>
    public required string APIKey { get; set; }

    /// <summary>
    /// Gets or sets the deployment model to be used for the text generation.
    /// </summary>
    public required string DeploymentName { get; set; }
}
