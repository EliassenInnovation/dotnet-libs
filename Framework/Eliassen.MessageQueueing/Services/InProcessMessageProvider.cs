﻿using Eliassen.MessageQueueing.Services;
using Eliassen.System.Text;
using Eliassen.System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Eliassen.Azure.StorageAccount.MessageQueueing;
/// <summary>
/// Represents an in-process message provider that implements both <see cref="IMessageSenderProvider"/> and <see cref="IMessageReceiverProvider"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="InProcessMessageProvider"/> class.
/// </remarks>
/// <param name="serializer">The JSON serializer.</param>
/// <param name="logger">The logger.</param>
public class InProcessMessageProvider(
    IJsonSerializer serializer,
    ILogger<InProcessMessageProvider> logger
    ) : IMessageSenderProvider, IMessageReceiverProvider
{
    private readonly ISerializer _serializer = serializer;
    private readonly ILogger _logger = logger;

    private IMessageHandlerProvider? _handlerProvider;
    private static readonly ConcurrentQueue<WrappedQueueMessage> _queue = new();

    /// <summary>
    /// Gets the key associated with the in-process message provider.
    /// </summary>
    public const string MessageProviderKey = "in-process";

    /// <inheritdoc/>
    public Task<string?> SendAsync(object message, IMessageContext context)
    {
        var wrapped = new WrappedQueueMessage
        {
            ContentType = "application/json;",
            PayloadType = message.GetType().AssemblyQualifiedName,
            CorrelationId = context.CorrelationId ?? Guid.NewGuid().ToString(), //TODO: do this better
            Payload = message,
            Properties = context.Headers,
        };
        _queue.Enqueue(wrapped);

        return Task.FromResult<string?>(wrapped.CorrelationId);
    }

    /// <inheritdoc/>
    public IMessageReceiverProvider SetHandlerProvider(IMessageHandlerProvider handlerProvider)
    {
        _handlerProvider = handlerProvider;
        return this;
    }

    /// <inheritdoc/>
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var newCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken).Token;

        if (_handlerProvider == null)
        {
            throw new ApplicationException($"{nameof(IMessageHandlerProvider)} is not provided");
        }

        while (!newCancellationToken.IsCancellationRequested)
        {
            if (!_queue.TryDequeue(out var wrapped))
            {
                _logger.LogInformation("Nothing received, waiting...");
                await Task.Delay(1000);  // TODO: this should be configurable
                continue;
            }

            await _handlerProvider.HandleAsync(wrapped, wrapped.CorrelationId);

            _logger.LogInformation($"Dequeue: {{CorrelationId}}", wrapped.CorrelationId);
        }
    }
}