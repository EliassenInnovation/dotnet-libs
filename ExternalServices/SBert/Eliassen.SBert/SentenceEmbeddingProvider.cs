﻿using Eliassen.AI;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Eliassen.SBert;

/// <summary>
/// Provides sentence embeddings using SBert.
/// </summary>
public class SentenceEmbeddingProvider : IEmbeddingProvider
{
    private readonly ISentenceEmbeddingClient _client;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SentenceEmbeddingProvider"/> class.
    /// </summary>
    /// <param name="client">The ISentenceEmbeddingClient for obtaining embeddings.</param>
    /// <param name="logger">The ILogger{SentenceEmbeddingProvider} instance for logging.</param>
    public SentenceEmbeddingProvider(
        ISentenceEmbeddingClient client,
        ILogger<SentenceEmbeddingProvider> logger
    )
    {
        _client = client;
        _logger = logger;
        logger.LogInformation($"connect to embeddings");
    }

    private int? _length;
    /// <summary>
    /// Gets the length of the embeddings.
    /// </summary>
    public int Length => _length ??= GetEmbeddingAsync("hello world", null).Result.Length;

    /// <summary>
    /// Gets the embedding for the given content asynchronously.
    /// </summary>
    /// <param name="content">The content for which to obtain the embedding.</param>
    /// <param name="model">The model for which to obtain the embedding.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the embedding as an array of floats.</returns>
    public Task<float[]> GetEmbeddingAsync(
        string content,
#if DEBUG
        string? model
#else
        string? model = default
#endif
        ) => _client.GetEmbeddingAsync(content);
}
